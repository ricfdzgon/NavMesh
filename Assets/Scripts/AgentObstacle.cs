using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentObstacle : MonoBehaviour
{
    public float damp = 4;
    public float displacement = 3f;
    float pingpong;
    float height;

    void Start()
    {
        height = transform.position.y;
    }

    void Update()
    {
        pingpong = Mathf.SmoothStep(-1, 1, Mathf.PingPong(Time.time / damp, 1));
        transform.localPosition = Vector3.up * height + transform.right * pingpong * displacement;
    }
}
