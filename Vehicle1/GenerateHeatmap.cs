using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//As perlin noise is pseudo-random, we will be using a randomly generated offset to ensure that we can create a believable random
//heatmap for our vehicle. Perlin noise is used to allow for smooth transitions in temperature across the whole heatmap

public class GenerateHeatmap : MonoBehaviour {
    //The width and height of our perlin noise heatmap
    public int width = 256;
    public int height = 256;

    //The scale or resolution at which we will write the perlin noise to the heatmap
    public float scale = 10f;

    //The offset of our perlin noise function
    public float offsetX = 1000f;
    public float offsetY = 1000f;

    //This will store our temperatures in a 2d array
    public float[,] temps;

    // Start is called before the first frame update
    void Start() {
        //Set our offsets to a random number to ensure our perlin noise is as random as we can get it
        offsetX = Random.Range(0, 9999f);
        offsetY = Random.Range(0, 9999f);

        temps = GenerateTemps();
    }

    float[,] GenerateTemps() {
        //Store a local 2d array of our temperatures and loop through our width and height generating a temperature for each spot in the array
        float[,] localtemps = new float[width,height];
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                localtemps[x,y] = GenerateTemp(x,y);
            }
        }
        return localtemps;
    }

    float GenerateTemp(int x, int y) {
        //Store a local location for use with the perlin noise function
        float localX = (float)x / width * scale + offsetX;
        float localY = (float)y / height * scale + offsetY;

        //return our float from the local location of the perlin noise function
        return Mathf.PerlinNoise(localX, localY);
    }

}
