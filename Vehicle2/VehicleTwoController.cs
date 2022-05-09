using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTwoController : MonoBehaviour {
    private Rigidbody rb;
    private GameObject sensorR;
    private GameObject sensorL;

    private List<float> distancesL;
    private List<float> distancesR;

    //Used to decide the type of vehicle eg "Fear", "Hate" and when paired with V3, "Love", "Explore"
    [SerializeField]
    public bool TypeToggle = false;
    //Used to decide if we're running vehicle 2 or 3 code
    [SerializeField]
    public bool V3 = false;

    // Start is called before the first frame update
    void Start() {
        //Set the vehicle components to the corresponding in-scene components and instantiate two lists which
        //will store the distances of all tracked lights from the left and right sensors
        rb = gameObject.GetComponent<Rigidbody>();
        sensorR = transform.Find("SensorR").gameObject;
        sensorL = transform.Find("SensorL").gameObject;

        distancesL = new List<float>();
        distancesR = new List<float>();
    }

    // Update is called once per frame
    void Update() {
        //Ensure the distance lists are clear before checking for lights in range
        distancesL.Clear();
        distancesR.Clear();
        //Loop through all lights in the scene
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Light")) {
            //Get the difference between the light's position and the vehicle's position
            Vector3 dif = obj.transform.position - gameObject.transform.position;
            //If the normalised difference is greater than 1.75, do not track the light, this ensures that only lights in front of the vehicle are tracked
            if(Vector3.Distance(dif.normalized, gameObject.transform.forward) > 1.75) continue;
            //Check the distance of the light from the vehicle, this is done using pythagoras' theorem (A^2 + B^2 = C^2)
            //If the distance is more than 40 units, don't track the light
            float dist = Mathf.Sqrt((dif.x * dif.x) + (dif.z * dif.z));
            if(dist > 40f) continue;

            //Get the distance of the light from each sensor and add it to the corresponding list
            Vector3 difL = obj.transform.position - sensorL.transform.position;
            Vector3 difR = obj.transform.position - sensorR.transform.position;
            distancesL.Add(Mathf.Sqrt((difL.x * difL.x) + (difL.z * difL.z)));
            distancesR.Add(Mathf.Sqrt((difR.x * difR.x) + (difR.z * difR.z)));
            //Get the direction and draw a ray for each sensor pointing to the light, differentiated by colour
            Vector3 dirL = difL.normalized;
            Vector3 dirR = difR.normalized;
            Debug.DrawRay(sensorL.transform.position, dirL, Color.blue);
            Debug.DrawRay(sensorR.transform.position, dirR, Color.red);
        }


        float speedL = 0f;
        float speedR = 0f;

        
        if(!V3) {
            //Run if this is a vehicle 2 simulation

            //Loop though the distance lists and work out the intensity of the light using the inverse square law
            //Some modifications were put in place to make the simulation smoother
            foreach (float dist in distancesL) {
                float distSqr = (dist * dist) > 1 ? (dist * dist) : 1;
                speedL += (1 + (1 / distSqr)) / distancesL.Count;
            }
            foreach(float dist in distancesR) {
                float distSqr = (dist * dist) > 1 ? (dist * dist) : 1;
                speedR += (1 + (1 / distSqr)) / distancesR.Count;
            }

            //Check if the vehicle is in Fear or Hate mode and decide how the vehicle responds to the intensity (turn towards or away)
            if(TypeToggle) {
                rb.velocity = (gameObject.transform.forward * speedL) + (gameObject.transform.forward * speedR);
                rb.AddTorque(Vector3.up * (speedL - speedR), ForceMode.Force);
            } else {
                rb.velocity = (gameObject.transform.forward * speedL) + (gameObject.transform.forward * speedR);
                rb.AddTorque(-(Vector3.up * (speedL - speedR)), ForceMode.Force);
            }
        } else {
            //Run if this is a vehicle 3 simulation

            //Loop though the distance lists and work out the intensity of the light using the inverse square law
            //and inverting the outcome to allow the vehicle to slow as it reaches the light
            //Some modifications were put in place to make the simulation smoother
            foreach (float dist in distancesL) {
                float distSqr = (dist * dist) > 1 ? (dist * dist) : 1;
                speedL += 1 - (1 / distSqr);
            }
            foreach(float dist in distancesR) {
                float distSqr = (dist * dist) > 1 ? (dist * dist) : 1;
                speedR += 1 - (1 / distSqr);
            }

            //Check if the vehicle is in Love or Explore mode and decide how the vehicle responds to the intensity (turn towards or away)
            if (TypeToggle) {
                rb.velocity = (gameObject.transform.forward * speedL) + (gameObject.transform.forward * speedR);
                rb.AddTorque(-(Vector3.up * (speedL - speedR)), ForceMode.Force);
            } else {
                rb.velocity = (gameObject.transform.forward * speedL) + (gameObject.transform.forward * speedR);
                rb.AddTorque(Vector3.up * (speedL - speedR), ForceMode.Force);
            }
        }
    }
}
