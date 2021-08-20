using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender1 : MonoBehaviour
{
    Animator animator;
    Transform transform;

    public GameObject ball;

    bool running = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (running)
        {
            transform.position = new Vector3(transform.position.x - 4 * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
    public void tackle()
    {
        animator.SetTrigger("Tackle");
        running = false;
    }

    public void newPosition()
    {
        running = true;
        transform.position = new Vector3(ball.transform.position.x + 8, 0, ball.transform.position.z - 2);
        animator.SetTrigger("Reset");
    }

}

