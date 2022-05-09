using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //The target to lock the camera to.
    public GameObject target;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        //Get the camera component and set its position to 20 units above the target
        //and rotate the camera to look down at the target
        cam = gameObject.GetComponent<Camera>();

        cam.transform.position = target.transform.position + new Vector3(0, 20, 0);
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = target.transform.position + new Vector3(0, 20, 0);
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
