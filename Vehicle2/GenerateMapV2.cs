using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapV2 : MonoBehaviour {    
    public GameObject lightPrefab;
    
    //Allows the user to toggle whether the script is active in the editor
    [SerializeField]
    public bool Toggle;

    // Start is called before the first frame update
    void Start() {
        if(!Toggle) return;
        //Loop 25 times in each 2d axis, skipping the direct centre which is where the vehicle will be.
        //on a 10% chance, spawn a lightPrefab at the current x*10, y*10 location, this allows for there to always be at least 10 units between lights
        for(int x = -25; x < 25; x++) {
            for(int y = -25; y < 25; y++) {
                if(x == 0 && y == 0) continue;
                if(Random.Range(0, 100) % 10 == 0)
                    Instantiate(lightPrefab, new Vector3(x*10,0.5f,y*10), Quaternion.Euler(Vector3.zero));
            }
        }
    }
}
