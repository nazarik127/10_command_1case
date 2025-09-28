using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MerkerManMovement : MonoBehaviour
{
    public float speed = 2f;
    public float runSpeed = 5f;
    public float gravity = -20f;
    public Transform cameraTransform; // Ссылка на камеру
    private CharacterController controller;
    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // В MerkerManMovement добавляем переменные
    public float jumpHeight = 1f;
    private bool jumpRequest = false;

    // Метод для запроса прыжка
    public void Jump()
    {
        if (controller.isGrounded)
        {
            // Рассчитываем стартовую скорость прыжка по формуле физики
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
            jumpRequest = true;
        }
    }

    void Update()
    {
        // --- движение остаётся как у тебя ---
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ += 1f;
        if (Input.GetKey(KeyCode.S)) moveZ -= 1f;
        if (Input.GetKey(KeyCode.A)) moveX -= 1f;
        if (Input.GetKey(KeyCode.D)) moveX += 1f;

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 moveDir = (camForward * moveZ + camRight * moveX).normalized;

        if (moveDir.magnitude > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 10f * Time.deltaTime);

        controller.Move(moveDir * currentSpeed * Time.deltaTime);
       
        float fallMultiplier = 2.5f;
        // --- гравитация и прыжок ---
        if (!controller.isGrounded)
            if (velocity.y < 0) // падаем вниз
                velocity.y += gravity * fallMultiplier * Time.deltaTime;
            else
                velocity.y += gravity * Time.deltaTime;
        else if (!jumpRequest)
            velocity.y = -2f;

        controller.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
        jumpRequest = false; // сбрасываем флаг
    }
}