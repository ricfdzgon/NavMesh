using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CentinelaMode : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform goal;
    public CentinelaAgentState agentState;
    public Vector3 destination;
    public GameObject puntoCentinela1, puntoCentinela2, puntoCentinela3;
    private bool puedeCazar;

    public float distance = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        puedeCazar = false;
        agentState = CentinelaAgentState.Iddle;
        destination = transform.position;
    }

    void FixedUpdate()
    {
        IsInRange();

        switch (agentState)
        {
            case CentinelaAgentState.Iddle:
                if (puedeCazar)
                {
                    SetState(CentinelaAgentState.Chasing);
                }

                break;
            case CentinelaAgentState.Centinel:
                if (puedeCazar)
                {
                    SetState(CentinelaAgentState.Chasing);
                }
                else
                {
                    if (agent.isStopped)
                    {
                        SetState(CentinelaAgentState.Iddle);
                    }
                }
                break;
            case CentinelaAgentState.Chasing:
                if (!puedeCazar)
                {
                    SetState(CentinelaAgentState.Centinel);
                }
                else
                {
                    destination = goal.position;
                }
                break;
        }
        agent.destination = destination;
    }

    void SetState(CentinelaAgentState newAgentState)
    {
        if (newAgentState != agentState)
        {
            Debug.Log(agentState);
            agentState = newAgentState;

            switch (agentState)
            {
                case CentinelaAgentState.Iddle:
                    //No me funciona este INVOKE
                    Invoke("CambiarPunto", 5f);
                    break;
                case CentinelaAgentState.Centinel:
                    CambiarPunto();
                    break;
                case CentinelaAgentState.Chasing:
                    break;
            }
        }
    }

    private int SorteoPunto()
    {
        return Random.Range(0, 3);
    }

    private void CambiarPunto()
    {
        Debug.Log("Cambié de punto");
        int punto = SorteoPunto();
        switch (punto)
        {
            case 0:
                destination = puntoCentinela1.transform.position;
                break;
            case 1:
                destination = puntoCentinela2.transform.position;
                break;
            case 2:
                destination = puntoCentinela3.transform.position;
                break;
        }
    }

    public void IsInRange()
    {
        Vector3 rayoIzquierdo = transform.forward * -30;
        Vector3 rayoDerecho = transform.forward * 30;
        RaycastHit hit;
        Debug.DrawLine(transform.position, goal.transform.position, Color.green, 1f);

        Debug.DrawRay(transform.position, transform.forward * 10, Color.red, 1f);
        //  Debug.DrawRay(transform.position, rayoIzquierdo * 10, Color.blue, 1f);
        // Debug.DrawRay(transform.position, rayoDerecho * 10, Color.magenta, 1f);

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

public enum CentinelaAgentState
{
    Iddle,
    Chasing,
    Returning,
    Centinel
}