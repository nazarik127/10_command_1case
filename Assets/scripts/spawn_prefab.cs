using UnityEngine;
using UnityEngine.AI;

public class NavMeshSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject enemyPrefab;
    public Transform playerTransform;


    [Header("Spawn settings")]
    public int spawnCount = 200;
    public float areaRadius = 50f;
    public Vector3 areaCenter = Vector3.zero;
    public bool useSphereArea = true;
    public Vector3 boxSize = new Vector3(100, 0, 100);

    [Header("Placement constraints")]
    public float minDistanceBetween = 1f;
    public int maxAttemptsPerEnemy = 30;
    public float navSampleMaxDistance = 2f;

    [Header("Hierarchy")]
    public Transform parentContainer;

    void Start()
    {
        if (parentContainer == null)
        {
            GameObject go = new GameObject("SpawnedEnemies");
            parentContainer = go.transform;
        }

        SpawnAll();
    }

    public void SpawnAll()
    {
        Vision vision = enemyPrefab.GetComponent<Vision>();
        if (vision != null)
        {
            vision.player = playerTransform;
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("NavMeshSpawner: enemyPrefab не задан.");
            return;
        }

        int spawned = 0;
        for (int i = 0; i < spawnCount; i++)
        {
            bool ok = TrySpawnOne(out GameObject spawnedGO);
            if (ok) spawned++;
            else Debug.LogWarning($"NavMeshSpawner: не удалось найти позицию для врага #{i + 1}");
        }

        Debug.Log($"NavMeshSpawner: попытались спавнить {spawnCount}, получилось {spawned}.");
    }

    bool TrySpawnOne(out GameObject spawnedGO)
    {
        spawnedGO = null;

        for (int attempt = 0; attempt < maxAttemptsPerEnemy; attempt++)
        {
            Vector3 randomPoint = useSphereArea ? RandomPointInSphere(areaCenter, areaRadius) : RandomPointInBox(areaCenter, boxSize);
            Vector3 sampleFrom = randomPoint + Vector3.up * 5f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(sampleFrom, out hit, navSampleMaxDistance + 5f, NavMesh.AllAreas))
            {
                Vector3 candidate = hit.position;

                if (!Physics.CheckSphere(candidate, minDistanceBetween))
                {
                    spawnedGO = Instantiate(enemyPrefab, candidate, Quaternion.identity, parentContainer);

                    return true;
                }
            }
        }

        return false;
    }

    Vector3 RandomPointInSphere(Vector3 center, float radius)
    {
        Vector2 r = Random.insideUnitCircle * radius;
        return new Vector3(center.x + r.x, center.y, center.z + r.y);
    }

    Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        float x = Random.Range(-size.x / 2f, size.x / 2f);
        float y = Random.Range(-size.y / 2f, size.y / 2f);
        float z = Random.Range(-size.z / 2f, size.z / 2f);
        return center + new Vector3(x, y, z);
    }

    public void ClearAndRespawn()
    {
        for (int i = parentContainer.childCount - 1; i >= 0; i--)
            DestroyImmediate(parentContainer.GetChild(i).gameObject);

        SpawnAll();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (useSphereArea)
            Gizmos.DrawWireSphere(areaCenter, areaRadius);
        else
            Gizmos.DrawWireCube(areaCenter, boxSize);
    }
}
