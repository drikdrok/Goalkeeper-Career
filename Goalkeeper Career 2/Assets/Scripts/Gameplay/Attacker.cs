using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Attacker : MonoBehaviour
{

    public BallScript ball;

    Animator animator;
    Transform transform;


    bool shooting = false;

    float startTimer = 0;
    bool waiting = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    public void newPosition()
    {
        animator.SetTrigger("Reset");
        Vector3 b = ball.transform.position;
        transform.position = new Vector3(b.x, 0, b.z + 2);

        waiting = true;
    }

    void shoot()
    {
        animator.SetTrigger("Shoot");
        shooting = true;
    }


    void Update()
    {
        if (shooting)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
            {
                ball.shoot();
                shooting = false;
            } 
        }

        if (waiting)
        {
            startTimer += Time.deltaTime;
            if (startTimer > 2)
            {
                waiting = false;
                startTimer = 0;
                shoot();
            }
        }
    }
}
