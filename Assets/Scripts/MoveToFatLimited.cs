using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToFatLimited : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform goal;
    public GameObject left;
    public GameObject right;
    public GameObject esfera;

    private Vector3 startPosition;
    private bool puedeCazar;

    public float distance = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        puedeCazar = false;
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
            Debug.Log("ANGULO " + angulo);
            Debug.Log("FORWARD" + transform.forward);
            if (angulo > -30 && angulo < 30)

                if (hit.transform.tag != "Obstaculo")
                {
                    Debug.Log("HOLI");
                    puedeCazar = true;
                }

        }
        else
        {
            RestartPosition();
            // Debug.Log("Did not Hit");
        }
    }
}