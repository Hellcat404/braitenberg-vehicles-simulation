using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLight : MonoBehaviour
{
    Camera cam;

    //Allows the user to set the lightPrefab in the editor
    [SerializeField]
    GameObject lightPrefab;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check check if the left mouse button is pressed and cast a ray from the camera to the mouse position. When the ray hits a collider,
        //such as the plane, instantiate a new lightPrefab at that location
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit)) {
                Instantiate(lightPrefab, new Vector3(hit.point.x, 0.5f, hit.point.z), Quaternion.Euler(0,0,0));
            }
        }
    }
}
