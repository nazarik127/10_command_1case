using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class RunBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private float attackRange = 2f;
    private float runRange = 20f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 20f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);

        // Проверка на атаку
        if (distance <= attackRange)
        {
            animator.SetTrigger("Attack");
            agent.SetDestination(agent.transform.position);
            SceneManager.LoadScene("menu");
            return;
        }

        // Дальше обычный бег
        if (distance < runRange)
            animator.SetBool("IsRun", true);
        else
            animator.SetBool("IsRun", false);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position); agent.speed = 2f;
    }
}