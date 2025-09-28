using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkBehaviour : StateMachineBehaviour
{
    float timer;
    NavMeshAgent agent;
    Transform player;
    float chaseRange = 10f;

    // Настройки генерации точки
    float wanderRadius = 20f; // радиус вокруг врага, где генерятся точки
    float timerLimit = 10f; // время до смены точки

    Vector3 GetRandomPointOnNavMesh(Vector3 origin, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return origin; // если не удалось найти, остаёмся на месте
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        agent = animator.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            Vector3 newPoint = GetRandomPointOnNavMesh(animator.transform.position, wanderRadius);
            agent.SetDestination(newPoint);
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null)
            return;

        // Если дошли до точки или таймер прошёл — выбираем новую
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 newPoint = GetRandomPointOnNavMesh(animator.transform.position, wanderRadius);
            agent.SetDestination(newPoint);
        }

        timer += Time.deltaTime;
        if (timer > timerLimit)
        {
            animator.SetBool("IsWalk", false);
        }

        // Проверка на игрока
        Vision vision = animator.GetComponent<Vision>();
        if (vision != null && vision.CanSeePlayer())
        {
            animator.SetBool("IsRun", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
            agent.SetDestination(agent.transform.position);
    }
}
