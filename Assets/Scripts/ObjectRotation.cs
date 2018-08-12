using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for rotating objects
public class ObjectRotation : MonoBehaviour {

    public float rotationSpeed = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
