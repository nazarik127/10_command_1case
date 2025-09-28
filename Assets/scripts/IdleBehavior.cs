using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    float timer;
    Transform player;
    float runRange = 10f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 5)
            animator.SetBool("IsWalk", true);

        Vision vision = animator.GetComponent<Vision>();
        if (vision != null && vision.CanSeePlayer())
            animator.SetBool("IsRun", true);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}