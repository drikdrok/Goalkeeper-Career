using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Attacker : MonoBehaviour
{

    public BallScript ball;
    public Defender1 defender1;

    Animator animator;
    Transform transform;


    bool running = false;
    float runningTimer = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    public void newPosition()
    {
        animator.SetTrigger("Reset");
        Vector3 b = ball.transform.position;
        transform.position = new Vector3(b.x, 0, b.z + 6.5f);

        running = true;
        runningTimer = 0;

    }

    public void shoot()
    {
        ball.shoot();
    }

    public void idle()
    {
        running = false;
    }

    void Update()
    {
        if (running)
        {
            runningTimer += Time.deltaTime;
            if (runningTimer >= 1f)
            {
                animator.SetTrigger("Shoot");
                runningTimer = -10000;
                defender1.tackle();
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, + transform.position.z - 4 * Time.deltaTime);
        }
    }
}
