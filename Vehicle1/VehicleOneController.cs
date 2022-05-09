using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the earliest vehicle I created, therefore some things are not quite as elegant as the later vehicles such as the Machina Speculatrix

public class VehicleOneController : MonoBehaviour {
    //Allows the user to set the vehicle's engine in the editor
    [SerializeField]
    private GameObject engine;

    //Allows the user to set the vehicle's sensor in the editor
    [SerializeField]
    private GameObject sensor;

    private Rigidbody rb;

    //stores the heatmap generated in a separate gameobject
    public float[,] heatmap;
    
    //Allows the user to set the vehicle's "tracker" gameobject which contains a line renderer that shows its path
    [SerializeField]
    private GameObject tracker;
    private LineRenderer lr;

    // Start is called before the first frame update
    void Start() {
        //Set the line renderer and set the width of the line
        lr = tracker.GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;

        //Get the heatmap wich was generated in a different gameobject
        heatmap = GameObject.Find("Heatmap").GetComponent<GenerateHeatmap>().temps;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        //Get the sensor's position mod 256 to ensure that it stays in the heatmap's ranges and ensure that the number given is always positive
        int sensorX = (int)sensor.transform.position.x % 256;
        int sensorY = (int)sensor.transform.position.z % 256;
        if(sensorX < 0)
            sensorX *= -1;
        if(sensorY < 0)
            sensorY *= -1;

        //Set the vehicle's velocity to the float stored at the corresponding location in the heatmap * 10 as the number will be between 1 and 0
        rb.velocity = gameObject.transform.forward * (heatmap[sensorX, sensorY]*10);
        //Rotate the vehicle a random amount around the y axis to create a pseudo brownian motion
        rb.MoveRotation(Quaternion.Euler(new Vector3(0,gameObject.transform.rotation.eulerAngles.y + Random.Range(-20f,20f),0)));

        //Sets the position of the line to be the engine's current position
        lr.positionCount++;
        lr.SetPosition(lr.positionCount-1, engine.transform.position);
        //Every 50 lines drawn, this will simplify the drawn line to keep down on memory usage
        if(lr.positionCount % 50 == 0)
            lr.Simplify(0.05f);
    }
}
