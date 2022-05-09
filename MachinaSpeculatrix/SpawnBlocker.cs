using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocker : MonoBehaviour
{
    Camera cam;

    //Allows the user to set the blockerPrefab in the editor
    [SerializeField]
    GameObject blockerPrefab;
    // Start is called before the first frame update
    void Start() {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        //Check check if the right mouse button is pressed and cast a ray from the camera to the mouse position. When the ray hits a collider,
        //such as the plane, instantiate a new blockerPrefab at that location
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                Instantiate(blockerPrefab, new Vector3(hit.point.x, 0.5f, hit.point.z), Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
