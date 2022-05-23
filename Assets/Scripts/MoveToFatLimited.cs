using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToFatLimited : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform goal;
    public AgentState agentState;
    public Vector3 destination;
    private Vector3 startPosition;
    private bool puedeCazar;

    public float distance = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // agent.destination = goal.position;
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        puedeCazar = false;
        agentState = AgentState.Iddle;
        destination = transform.position;
    }

    void FixedUpdate()
    {
        IsInRange();

        switch (agentState)
        {
            case AgentState.Iddle:
                if (puedeCazar)
                {
                    SetState(AgentState.Chasing);
                }
                break;
            case AgentState.Chasing:
                if (!puedeCazar)
                {
                    SetState(AgentState.Returning);
                }
                else
                {
                    destination = goal.position;
                }
                break;
            case AgentState.Returning:
                if (puedeCazar)
                {
                    SetState(AgentState.Chasing);
                    break;
                }
                else
                {
                    if (agent.isStopped)
                    {
                        SetState(AgentState.Iddle);
                    }
                }
                break;
        }
        agent.destination = destination;
    }

    void SetState(AgentState newAgentState)
    {
        if (newAgentState != agentState)
        {
            Debug.Log(agentState);
            agentState = newAgentState;

            switch (agentState)
            {
                case AgentState.Iddle:
                    destination = transform.position;
                    break;

                case AgentState.Chasing:
                    break;
                case AgentState.Returning:
                    destination = startPosition;
                    break;
            }
        }
    }

    public void IsInRange()
    {
        Vector3 rayoIzquierdo = transform.forward * -30;
        Vector3 rayoDerecho = transform.forward * 30;
        RaycastHit hit;
        Debug.DrawLine(transform.position, goal.transform.position, Color.green, 1f);

        Debug.DrawRay(transform.position, transform.forward * 10, Color.red, 1f);
        Debug.DrawRay(transform.position, rayoIzquierdo * 10, Color.blue, 1f);
        Debug.DrawRay(transform.position, rayoDerecho * 10, Color.magenta, 1f);

        if (Physics.Raycast(transform.position, goal.transform.position - transform.position, out hit, distance))
        {
            Vector3 targetDir = goal.transform.position - transform.position;
            float angulo = Vector3.Angle(transform.forward, targetDir);
            if (angulo > -30 && angulo < 30)

                if (hit.transform.tag != "Obstaculo")
                {
                    puedeCazar = true;
                }
        }
        else
        {
            puedeCazar = false;
        }
    }
}

public enum AgentState
{
    Iddle,
    Chasing,
    Returning
}