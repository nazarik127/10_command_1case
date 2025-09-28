using UnityEngine;

public class MerkerManController : MonoBehaviour
{
    public Animator Animator;

    private bool isJumping = false;

    void Update()
    {
        if (!Animator) return;

        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);

        bool isWalking = w || a || s || d;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isWalking;
        bool isJumping = Input.GetKeyDown(KeyCode.Space); // только при нажатии

        Animator.SetBool("isWalking", isWalking);
        Animator.SetBool("isRunning", isRunning);
        Animator.SetBool("isJumping", isJumping);

        if (isJumping)
        {
            GetComponent<MerkerManMovement>().Jump();
        }
    }

}
