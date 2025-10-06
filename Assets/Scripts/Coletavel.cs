using UnityEngine;
using System.Collections;

public class Coletavel : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public BoxCollider2D spawnArea;
    public int maxActive = 5;
    public float spawnInterval = 1.0f;
    public float minDistanceFromPlayer = 0.8f;
    public LayerMask blockMask;
    public float overlapRadius = 0.3f;

    Transform player;

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) {
            player = p.transform;
        }
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (GameTimer.timeOver || GameController.gameOver) { yield return null; continue; }

            int active = GameObject.FindGameObjectsWithTag("Coletavel").Length;
            if (active < maxActive){
                TrySpawn();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void TrySpawn()
    {
        if (collectiblePrefab == null || spawnArea == null){
            return;
        }

        Bounds b = spawnArea.bounds;
        for (int i = 0; i < 10; i++){
            float x = Random.Range(b.min.x, b.max.x);
            float y = Random.Range(b.min.y, b.max.y);
            Vector2 pos = new Vector2(x, y);

            if (player != null && Vector2.Distance(pos, player.position) < minDistanceFromPlayer){
                continue;
            }

            if (Physics2D.OverlapCircle(pos, overlapRadius, blockMask) != null){
                continue;
            }

            Instantiate(collectiblePrefab, pos, Quaternion.identity);
            return;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (spawnArea == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnArea.bounds.center, spawnArea.bounds.size);
    }
}
