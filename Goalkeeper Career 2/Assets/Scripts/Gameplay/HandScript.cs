using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    // Start is called before the first frame update


    public new Transform transform;

    public Animator animator;

    public bool caughtBall = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Set position
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 4;
       
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        transform.position = newPos;

        //Set rotation

        Vector3 newRotation = new Vector3(0,0,0);
        if (transform.position.x > 2)
        {
            newRotation = new Vector3(0, 0, -(transform.position.x - 2) * 180 / Mathf.PI);
        }else if (transform.position.x < -2)
        {
            newRotation = new Vector3(0, 0, (Mathf.Abs(transform.position.x) - 2) * 180 / Mathf.PI);
        }
        
        transform.rotation = Quaternion.Euler(newRotation);



        animator.SetBool("Caught", caughtBall);
    }
}
