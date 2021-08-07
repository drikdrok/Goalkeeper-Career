using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    private new Rigidbody rigidbody;
    private new Transform transform;
    
    public HandScript hands;
    public GameScript gameScript;

    public Transform handsTransform;

    float age = 0;
    bool beenShot = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            rigidbody.AddForce(new Vector3(Random.Range(-300, 300), Random.Range(100, 300) , Random.Range(-500, -800)));
            beenShot = true;
        }

        if (transform.position.z < 40 || Input.GetKeyDown("r"))
        {
            newPosition();
        }

        if (hands.caughtBall)
        {
            transform.position = handsTransform.position;
        }

        if (beenShot)
        {
            age += Time.deltaTime;
            if (age >= 2.5)
            {
                gameScript.saveStreak++;
                if (hands.caughtBall)
                    gameScript.saveStreak++;

                newPosition();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CatchTrigger")
        {
            rigidbody.isKinematic = true;
            hands.caughtBall = true;
        }else if (other.tag == "GoalTrigger")
        {
            newPosition();
            gameScript.saveStreak = 0;
            if (gameScript.playerHome)
                gameScript.awayScore++;
            else
                gameScript.homeScore++;
        }
    }


    void newPosition()
    {
        transform.position = new Vector3(Random.Range(-4, 4), 0.16f, Random.Range(50, 55));
        rigidbody.velocity = new Vector3(0, 0, 0);
        hands.caughtBall = false;
        rigidbody.isKinematic = false;

        age = 0;
        beenShot = false;

        gameScript.newPosition();

    }


}
