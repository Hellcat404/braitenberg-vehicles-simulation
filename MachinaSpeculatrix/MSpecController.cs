using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpecController : MonoBehaviour
{
    //Store the vehicle's components
    private Rigidbody rb;
    private GameObject sensor;

    //Store script variables
    private Ray sensorRay;
    private RaycastHit sensorHit;
    private bool blocked = false;

    // Start is called before the first frame update
    void Start()
    {
        //Set the vehicle components to their corresponding in-scene components
        rb = gameObject.GetComponent<Rigidbody>();
        sensor = transform.Find("Sensor").gameObject;
        //Create a new ray from the sensor and in the forward direction from the vehicle's perspective
        sensorRay = new Ray(sensor.transform.position, gameObject.transform.forward);
    }

    void FixedUpdate()
    {
        //Add a force of ~0.5 units in the sensorRay's direction at the sensor's position to the vehicle
        //This direction will start at the forward direction of the vehicle
        //This is done at the start as the "wheel" should always be turning, moving the vehicle in the direction of the "wheel"
        rb.AddForceAtPosition(sensorRay.direction / 2, sensor.transform.position);
        //Ensure the sensorRay's origin is from the sensor's position at all times
        sensorRay.origin = sensor.transform.position;
        //If the vehicle cannot move because it is colliding with a "blocker", the sensor's ray doesn't hit anything,
        //the sensor's ray hits a "blocker" or the sensor's ray hits the vehicle's body
        //We will rotate the sensor's ray around the Y axis 5 degrees and draw the ray in a red colour for visual feedback
        if (blocked || !Physics.Raycast(sensorRay, out sensorHit) || sensorHit.transform.gameObject.tag == "Blocker" || sensorHit.transform.gameObject == gameObject)
        {
            sensorRay.direction = Quaternion.Euler(0, 5, 0) * sensorRay.direction;
            Debug.DrawRay(sensor.transform.position, sensorRay.direction, Color.red);
            return;
        }
        rb.AddForceAtPosition(sensorRay.direction / 2, sensor.transform.position);
        //Draw the sensorRay in green to show that a light is detected
        Debug.DrawRay(sensor.transform.position, sensorRay.direction, Color.green);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the vehicle collides with a "blocker" set the blocked boolean to true
        if (collision.gameObject.tag == "Blocker")
            blocked = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        //When the vehicle is no longer colliding with anything, set blocked to false
        blocked = false;
    }
}
