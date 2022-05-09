using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Most uncommented code is from the VehicleTwoContoller script

public class VehicleFourController : MonoBehaviour {

    private Rigidbody rb;
    private GameObject sensorR;
    private GameObject sensorL;
    private GameObject engineR;
    private GameObject engineL;

    private List<float> distancesL;
    private List<float> distancesR;

    //The engine power lookup array (Sine wave 0-180deg) this will allow less Mathf.Sin function calls during update
    float[] pwrArray = new float[180];

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        sensorR = transform.Find("SensorR").gameObject;
        sensorL = transform.Find("SensorL").gameObject;
        engineR = transform.Find("EngineR").gameObject;
        engineL = transform.Find("EngineL").gameObject;

        distancesL = new List<float>();
        distancesR = new List<float>();

        //Loop through the pwrArray and add the corresponding sin outputs to it
        for(int i = 0; i < pwrArray.Length; i++) {
            pwrArray[i] = Mathf.Sin((i * Mathf.PI) / 180);
        }
    }

    void FixedUpdate() {
        distancesL.Clear();
        distancesR.Clear();

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Light")) {
            Vector3 dif = obj.transform.position - gameObject.transform.position;
            float dist = Mathf.Sqrt((dif.x * dif.x) + (dif.z * dif.z));
            if(dist > 40f) continue;

            Vector3 difL = obj.transform.position - sensorL.transform.position;
            Vector3 difR = obj.transform.position - sensorR.transform.position;
            distancesL.Add(Mathf.Sqrt((difL.x * difL.x) + (difL.z * difL.z)));
            distancesR.Add(Mathf.Sqrt((difR.x * difR.x) + (difR.z * difR.z)));
            Vector3 dirL = difL.normalized;
            Vector3 dirR = difR.normalized;
            Debug.DrawRay(sensorL.transform.position, dirL, Color.blue);
            Debug.DrawRay(sensorR.transform.position, dirR, Color.red);
        }

        float sensorLIntensity = 0.0f;
        float sensorRIntensity = 0.0f;

        //Intensity is calculated in mostly the same way as Vehicle 2 but it multiplied by 1000 to allow for better use of the pwrArray
        foreach(float dist in distancesL) {
            float distSqr = dist * dist;
            sensorLIntensity += (1 / distSqr)*1000;
        }
        foreach(float dist in distancesR) {
            float distSqr = dist * dist;
            sensorRIntensity += (1 / distSqr)*1000;
        }
        //sensorIntensity mod 180 is used to ensure we do not run over the pwrArray index limit
        float speedL = pwrArray[(int)sensorLIntensity % 180];
        float speedR = pwrArray[(int)sensorRIntensity % 180];

        //Add force corresponding to both engine speeds in the vehicle forward direction at the corresponding engine locations
        rb.AddForceAtPosition((gameObject.transform.forward * speedL), engineR.transform.position);
        rb.AddForceAtPosition((gameObject.transform.forward * speedR), engineL.transform.position);
    }
}
