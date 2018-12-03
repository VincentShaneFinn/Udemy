using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody myRigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 50f;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Friendly":
                print("Friendly"); //todo remove
                break;
            case "Fuel":
                print("Fuel"); //todo remove
                break;
            default:
                Destroy(gameObject); //todo kill player
                break;
        }
    }

    private void Thrust()
    {

        if (Input.GetKey(KeyCode.Space)) //Can thrust while rotating
        {
            myRigidbody.AddRelativeForce(Vector3.up * mainThrust); //so this should always be thrusting up
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    private void Rotate()
    {
        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
            myRigidbody.freezeRotation = true; //take manual controll of rotation
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
            myRigidbody.freezeRotation = true; //take manual controll of rotation
        }

        myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
