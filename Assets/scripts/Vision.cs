using UnityEngine;

public class Vision : MonoBehaviour
{
    public float viewAngle = 90f;    // угол обзора
    public float viewDistance = 10f; // дальность
    public Transform player;         // ссылка на игрока

    public bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        float distance = Vector3.Distance(transform.position, player.position);

        if (angle < viewAngle / 2f && distance < viewDistance)
        {
            // Проверка препятствий
            if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, viewDistance))
            {
                return hit.collider.CompareTag("Player");
            }
        }
        return false;
    }

    // Для наглядности в Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 left = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + left * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + right * viewDistance);
    }
}
