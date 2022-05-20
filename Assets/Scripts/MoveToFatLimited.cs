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
        RaycastHit hit;
        Debug.DrawLine(transform.position, goal.transform.position, Color.green, 1f);
        if (Physics.Raycast(transform.position, goal.transform.position - transform.position, out hit, distance))
        {
            Debug.Log("HIT" + hit.transform.eulerAngles.magnitude);
            Debug.Log("DERECHA " + right.transform.eulerAngles.magnitude);
            Debug.Log("IZQUIERDA " + left.transform.eulerAngles.magnitude);
            Debug.Log("ESFERA " + esfera.transform.eulerAngles.normalized.magnitude);

            if (hit.transform.eulerAngles.magnitude >= left.transform.eulerAngles.magnitude && hit.transform.eulerAngles.magnitude <= 290)
            {
                Debug.Log("Entré en el lio de angulos");
                if (hit.transform.tag != "Obstaculo")
                {
                    Debug.Log("HOLI");
                    puedeCazar = true;
                }
            }
        }
        else
        {
            RestartPosition();
            // Debug.Log("Did not Hit");
        }
    }
}