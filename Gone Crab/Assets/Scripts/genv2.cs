using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genv2 : MonoBehaviour
{

    //Public Variables
    public int ObjectsToSpawn;
    public GameObject[] TerrainObjects;
    public string SpawnedItemName;
    public string ObjectsWillSpawnOnTag1;
    public string ObjectsWillSpawnOnTag2;
    public string ObjectsWillSpawnOnTag3;
    public string ObjectsWillSpawnOnTag4;
    public string ObjectsWillSpawnOnTag5;
    public string ObjectSpawnedTag;
    

    //object scaling variables
    private float BoxX;
    private float BoxZ;
    private float BoxY;

    public float MinSize = 1;
    public float MaxSize = 1;

    public bool DestroyOverlap = true;
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
                TerrainPlacement.transform.position = new Vector3(Random.Range(TerrainPlacement.transform.position.x - BoxX / 2, TerrainPlacement.transform.position.x + BoxX / 2), BoxY, Random.Range(TerrainPlacement.transform.position.z - BoxZ / 2, TerrainPlacement.transform.position.z + BoxZ / 2));
                //raycast to see if the object will be spawned on land tagged "Terrain"
                Vector3 fwd = TerrainPlacement.transform.TransformDirection(Vector3.down);

                if (Physics.Raycast(TerrainPlacement.transform.position, fwd, out RayhitObject))
                {
                    if (RayhitObject.transform.tag == ObjectsWillSpawnOnTag1 || RayhitObject.transform.tag == ObjectsWillSpawnOnTag2 || RayhitObject.transform.tag == ObjectsWillSpawnOnTag3 || RayhitObject.transform.tag == ObjectsWillSpawnOnTag4 || RayhitObject.transform.tag == ObjectsWillSpawnOnTag5)
                    {
                        int RND = Random.Range(0, ArrayLength);

                        GameObject newobject = Instantiate(TerrainObjects[RND], new Vector3(0, 0, 0), TerrainPlacement.transform.rotation);

                        float RND2 = Random.Range(newobject.transform.localScale.x * MinSize, newobject.transform.localScale.x * MaxSize);

                        newobject.transform.localScale = new Vector3(newobject.transform.localScale.x * RND2, newobject.transform.localScale.y * RND2, newobject.transform.localScale.z * RND2);

                        newobject.transform.position = new Vector3(TerrainPlacement.transform.position.x, RayhitObject.point.y + newobject.transform.localScale.y / 2 - SpawnedItemHeightOffset, TerrainPlacement.transform.position.z);

                        newobject.name = SpawnedItemName;

                        //dont forget the layer has to exsist
                        newobject.tag = ObjectSpawnedTag;
                        
                        if (DestroyOverlap)
                        {
                            UnityEngine.Collider[] hitColliders = Physics.OverlapSphere(newobject.transform.position, newobject.transform.localScale.x * SpawnCollisionDeleteRadius);
                            if (hitColliders.Length > 2)
                            {
                                Destroy(newobject);
                            }
                        }
                    }

                }
                TerrainPlacement.transform.position = originalxz;
            }
        }
    }
    //vector3.forward is z
    //vector3.right is x +
}
   
       


