using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audioMoeda;
    AudioSource audioInimigo;
    public float speed;
    private float originalSpeed;
    private bool touchingEnemy;
    public float hitCooldown = 0.5f;
    private float lastHitTime;
    public float lossPerHit = 2f;
    public float growthPerPickup = 0.1f;
    public  float maxScale = 3.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AudioSource[] sources = GetComponents<AudioSource>();
        audioMoeda = sources[0];
        audioInimigo = sources[1];
        originalSpeed = speed;
        touchingEnemy = false;
        lastHitTime = -999f;
    }

    void FixedUpdate()
    {
        if (touchingEnemy){
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coletavel")
        {
            if (other.enabled == false) return;
            other.enabled = false;

            audioMoeda.Play();
            GameController.Collect();
            Vector3 s = transform.localScale;
            s += Vector3.one * growthPerPickup;
            float clamped = Mathf.Min(s.x, maxScale);
            transform.localScale = new Vector3(clamped, clamped, 0.5f);
            var timer = Object.FindFirstObjectByType<GameTimer>();
            if (timer != null){
                timer.AddBonus(2f);
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Inimigo")){
            touchingEnemy = true;
            speed = 0f;
            if (Time.time - lastHitTime >= hitCooldown){
                audioInimigo.Play();
                var timer = Object.FindFirstObjectByType<GameTimer>();
                if (timer != null){
                    timer.AddBonus(-lossPerHit);
                }
                lastHitTime = Time.time;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Inimigo"))
        {
            touchingEnemy = true;
            speed = 0f;
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Inimigo"))
        {
            touchingEnemy = false;
            speed = originalSpeed;
        }
    }
}
