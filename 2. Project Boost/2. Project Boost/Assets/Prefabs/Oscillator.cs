using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // ensures you cant have more than one of this script on an object
public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    // todo remove from inspector later
    [Range(0, 1)] [SerializeField] float movementFactor;

    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // if the period float is less than the smallest possible thing
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // grows continually from 0;

        const float tau = Mathf.PI * 2; // we know this value will not change at run time, and is about 6.28

        float rawSinWave = Mathf.Sin(cycles * tau);

        print(rawSinWave); // 0 - 1 - -1 - 0 etc

        //need it to go between 0 and 1
        //so / 2 makes it -.5 to .5 and then adding .5 makes it 0 to 1
        movementFactor = rawSinWave / 2f + .5f; // plus the amplitude


        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;


	}
}
