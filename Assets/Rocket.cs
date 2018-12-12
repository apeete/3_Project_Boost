using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //Game State Variables
    [SerializeField]float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    //Game Mechanic Variables
    Rigidbody rigidBody;
    AudioSource rocketThrust;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketThrust = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust(); 
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("WE GOOD"); //todo: remove line
                break;
            case "Fuel":
                print("GASSED UP"); //todo: remove line
                break;
            default:
                print("HIT");
                break;
        }
    }

    private void Thrust()
    {
        //float thrustOnFrame = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

            if (!rocketThrust.isPlaying) //prevent sound layering
            {
                rocketThrust.Play();
            }
        }
        else
        {
            rocketThrust.Stop();
        }
    }

    private void Rotate()
    {
        float rotationOnFrame = rcsThrust * Time.deltaTime;

        rigidBody.freezeRotation = true; //take manual control of rotation

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationOnFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationOnFrame);
        }

        rigidBody.freezeRotation = false; //resume physics control of rotation
    }
}
