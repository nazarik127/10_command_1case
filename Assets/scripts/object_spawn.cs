using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshSpawner : MonoBehaviour
{
    [Header("Префабы для спавна")]
    public GameObject[] prefabs; // массив префабов, любой может быть

    [Header("Количество объектов")]
    public int spawnCount = 30;

    [Header("Центр и радиус")]
    public Vector3 center = Vector3.zero;
    public float radius = 50f;

    [Header("NavMesh")]
    public float sampleRadius = 2f; // радиус для поиска точки на NavMesh

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        if (prefabs.Length == 0)
        {
            Debug.LogWarning("Нет префабов для спавна!");
            return;
        }

        int spawned = 0;
        int attempts = 0;
        int maxAttempts = spawnCount * 10;

        while (spawned < spawnCount && attempts < maxAttempts)
        {
            attempts++;

            // случайная точка в круге по XZ
            Vector2 randomCircle = Random.insideUnitCircle * radius;
            Vector3 randomPos = center + new Vector3(randomCircle.x, 0, randomCircle.y);

            // ищем ближайшую точку на NavMesh
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, sampleRadius, NavMesh.AllAreas))
            {
                // выбираем рандомный префаб
                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
                Instantiate(prefab, hit.position, Quaternion.identity);
                spawned++;
            }
        }

        Debug.Log($"✅ Успешно заспавнено: {spawned}/{spawnCount} объектов");
    }

    // рисуем круг в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, radius);
    }
}
