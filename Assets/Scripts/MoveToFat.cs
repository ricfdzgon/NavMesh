using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToFat : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform goal;
    private Vector3 startPosition;
    private bool puedeCazar;

    public float distance = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        startPosition = SceneManager.instance.startPositionSeguidor.position;
        puedeCazar = true;
    }

    void Update()
    {
        if (puedeCazar)
        {
            agent.destination = goal.position;
        }
        else
        {
            RestartPosition();
        }

    }

    private void RestartPosition()
    {
        agent.destination = startPosition;
        puedeCazar = false;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (goal.transform.position - transform.position).normalized, out hit, distance))
        {
            if (hit.transform.tag != "Obstaculo")
            {
                puedeCazar = true;
                Debug.Log("Did Hit");
            }

        }
        else
        {
            RestartPosition();
            Debug.Log("Did not Hit");
        }
    }
}
