using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BallScript : MonoBehaviour
{

    private new Rigidbody rigidbody;
    private new Transform transform;
    
    public HandScript hands;
    public GameScript gameScript;

    public Transform handsTransform;

    float age = 0;
    bool beenShot = false;

    public Attacker attacker1;
    public Defender1 defender1;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        
        newPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //shoot();
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
                PlayerPrefs.SetInt("TotalSaves", PlayerPrefs.GetInt("TotalSaves", 0) + 1);
                if (hands.caughtBall)
                {
                    gameScript.saveStreak++;
                    PlayerPrefs.SetInt("TotalCatches", PlayerPrefs.GetInt("TotalCatches", 0) + 1);

                }

                newPosition();
            }
        }

    }

    public void shoot()
    {
        rigidbody.AddForce(new Vector3(Random.Range(-300, 300), Random.Range(100, 300), Random.Range(-500, -800)));
        beenShot = true;
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
            PlayerPrefs.SetInt("TotalConceeded", PlayerPrefs.GetInt("TotalConceeded", 0) + 1);
            if (gameScript.playerHome)
                gameScript.awayScore++;
            else
                gameScript.homeScore++;
        }
    }


    void newPosition()
    {
        
        transform.position = new Vector3(Random.Range(-4, 4), 0.16f, Random.Range(50, 61));
        rigidbody.velocity = new Vector3(0, 0, 0);
        attacker1.newPosition();
        defender1.newPosition();

        if (Random.value < 0.5f)
        {
            transform.position = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
            rigidbody.velocity = new Vector3(-4.8f, 0, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
            rigidbody.velocity = new Vector3(4.8f, 0, 0);
        }

        rigidbody.angularVelocity = new Vector3(0, 0, 0);
        hands.caughtBall = false;
        rigidbody.isKinematic = false;

        age = 0;
        beenShot = false;


        gameScript.newPosition();

    }


}
