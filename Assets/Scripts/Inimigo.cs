using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 15f;

    Rigidbody2D rb;
    Transform target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
        }
    }

    void Start()
    {
        if (pointB != null){
            target = pointB;
        }
        else{
            target = pointA;
        }
    }

    void FixedUpdate()
    {
        if (rb == null) {
            return;
        }
        if (pointA == null || pointB == null || target == null){
            return;
        }
        Vector2 pos = rb.position;
        Vector2 dir = ((Vector2)target.position - pos).normalized;

        rb.MovePosition(pos + dir * speed * Time.fixedDeltaTime);

        if (Vector2.Distance(pos, target.position) <= 0.05f)
        {
            if (target == pointA){
                target = pointB;
            }
            else{
                target = pointA;
            }
        }
    }
}
