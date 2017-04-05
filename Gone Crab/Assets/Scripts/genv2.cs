using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genv2 : MonoBehaviour
{

    //Public Variables
    public int ObjectsToSpawn;
    public GameObject[] TerrainObjects;
    public string TerrainName;

    //object scaling variables
    private float BoxX;
    private float BoxZ;
    private float BoxY;

    public float SpawnCollisionDeleteRadius = 1;

    //raycast variables
    private RaycastHit RayhitObject;


    //Object Spawn Height offset
    public float SpawnedItemHeightOffset = 1;
 



    //private variables

    //working with x and y for placement
    public GameObject TerrainPlacement = null;

    // Use this for initialization
    void Start()
    {
        this.transform.rotation = TerrainPlacement.transform.rotation;
        //get the scale of the object
        BoxX = TerrainPlacement.transform.localScale.x;
        BoxZ = TerrainPlacement.transform.localScale.z;
        BoxY = TerrainPlacement.transform.position.y;

        int ArrayLength = TerrainObjects.Length;
        for (int i = 0; i < ObjectsToSpawn; i++)
        {
            Vector3 originalxz = TerrainPlacement.transform.position;
            //generate a random position based on generation radius
            //TerrainPlacement.transform.position = new Vector3(Random.Range(this.transform.position.x - BoxX / 2, this.transform.position.x + BoxX / 2), BoxY, Random.Range(this.transform.position.z - BoxZ / 2, this.transform.position.z + BoxZ / 2));
            TerrainPlacement.transform.Translate(Vector3.forward * Random.Range(TerrainPlacement.transform.position.z - BoxZ / 2, TerrainPlacement.transform.position.z + BoxZ / 2));
            TerrainPlacement.transform.Translate(Vector3.right * Random.Range(TerrainPlacement.transform.position.x - BoxX / 2, TerrainPlacement.transform.position.x+ BoxX / 2));


            //raycast to see if the object will be spawned on land tagged "Terrain"
            Vector3 fwd = TerrainPlacement.transform.TransformDirection(Vector3.down);

            if (Physics.Raycast(TerrainPlacement.transform.position, fwd, out RayhitObject))
            {
                if (RayhitObject.transform.tag == "Terrain")
                {
                    
                    int RND = Random.Range(0, ArrayLength);
                    GameObject newobject = Instantiate(TerrainObjects[RND], new Vector3(0, 0, 0), TerrainPlacement.transform.rotation);
                    newobject.transform.position = new Vector3(TerrainPlacement.transform.position.x, RayhitObject.point.y + newobject.transform.lossyScale.y / 2 - SpawnedItemHeightOffset, TerrainPlacement.transform.position.z);

                    newobject.name = TerrainName;
                    newobject.tag = "Spawned";

                    UnityEngine.Collider[] hitColliders = Physics.OverlapSphere(newobject.transform.position, SpawnCollisionDeleteRadius);
                    if (hitColliders.Length > 2)
                    {
                        Debug.Log("Object Destroyed");
                        Destroy(newobject);
                    }
                }
            }
            TerrainPlacement.transform.position = originalxz;
        }
    }
    //vector3.forward is z
    //vector3.right is x +

    // Update is called once per frame
    void Update()
    {          
    }
}
