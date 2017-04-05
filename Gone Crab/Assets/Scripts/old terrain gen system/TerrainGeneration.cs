using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    //Public Variables
    public int ObjectsToSpawn;
    public GameObject[] TerrainObjects;
    public string TerrainName;

    //object scaling variables
    private float BoxX;
    private float BoxZ;
    private float BoxY;


    //private variables

    //working with x and y for placement
    public GameObject TerrainPlacement;

    // Use this for initialization
    void Start()
    {
        //get the scale of the object
        BoxX = TerrainPlacement.transform.localScale.x;
        BoxZ = TerrainPlacement.transform.localScale.z;
        BoxY = TerrainPlacement.transform.position.y;

        int ArrayLength = TerrainObjects.Length;
        for (int i = 0; i <= ObjectsToSpawn; i++)
        {
            Vector3 originalxz = TerrainPlacement.transform.position;
            //generate a random position based on generation radius
            TerrainPlacement.transform.position = new Vector3(Random.Range(this.transform.position.x - BoxX/2, this.transform.position.x + BoxX/2), BoxY, Random.Range(this.transform.position.z - BoxZ/2, this.transform.position.z + BoxZ / 2));
            int RND = Random.Range(0, ArrayLength);
            var newobject = Instantiate(TerrainObjects[RND], TerrainPlacement.transform.position, TerrainPlacement.transform.rotation);
            newobject.name = TerrainName;
            TerrainPlacement.transform.position = originalxz;
        }
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}