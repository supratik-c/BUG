using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;

    public int numToCopy;

    public float spawnSphereRadius;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< numToCopy; i++) 
        {
            Instantiate(ObjectToSpawn, Random.insideUnitSphere * spawnSphereRadius , Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
