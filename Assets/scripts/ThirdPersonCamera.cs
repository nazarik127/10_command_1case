using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float smoothSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float pivotHeight = 1.7f;
    public LayerMask collisionMask;
    public float sphereRadius = 0.3f;

    private float yaw = 0f;
    private float pitch = 0f;

    void LateUpdate()
    {
        if (!target) return;

        // Двигаем мышью
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // ❌ Без clamp — крутим как хотим
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Точка вращения (примерно уровень головы)
        Vector3 pivot = target.position + Vector3.up * pivotHeight;
        Vector3 desiredPosition = pivot + rotation * offset;

        // Проверяем препятствия (в том числе пол)
        RaycastHit hit;
        Vector3 direction = (desiredPosition - pivot).normalized;
        float distance = offset.magnitude;

        if (Physics.SphereCast(pivot, sphereRadius, direction, out hit, distance, collisionMask))
        {
            // Если что-то мешает — ставим камеру перед препятствием
            desiredPosition = hit.point - direction * 0.3f;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(pivot);
    }
}
