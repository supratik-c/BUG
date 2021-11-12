using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomLists : MonoBehaviour
{
    public int MaxRoomsToSpawn;

    public int SpawnedRooms = 0;

    public List<GameObject> BottomRooms = new List<GameObject>();
    public List<GameObject> TopRooms = new List<GameObject>();
    public List<GameObject> LeftRooms = new List<GameObject>();
    public List<GameObject> RightRooms = new List<GameObject>();

    private bool spawningDone;

    public NavMeshSurface _NavMeshSurface;

	private void Awake()
	{
        SpawnedRooms = 0;
	}

	private void Start()
	{
        StartCoroutine(spawnRooms());
	}

	IEnumerator spawnRooms() 
    {
        while (SpawnedRooms < MaxRoomsToSpawn || spawningDone) 
        {
            List<RoomSpawner> ActiveSpawnPoints = new List<RoomSpawner>();

            ActiveSpawnPoints.AddRange(FindObjectsOfType<RoomSpawner>());

            if (ActiveSpawnPoints.Count == 0) 
            {
                spawningDone = true;

                Debug.Log("Found No Spawn Points");
                _NavMeshSurface.BuildNavMesh();
                yield break;
            }

            foreach (RoomSpawner spawner in ActiveSpawnPoints) 
            {
                if (spawner != null && !spawner.Spawned) 
                {
                    spawner.Spawn();
                    //yield return new WaitForFixedUpdate()
                    yield return new WaitForEndOfFrame();
                    //if (spawner.gameObject != null) 
                    { 
                        //Destroy(spawner.gameObject);
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }
        _NavMeshSurface.BuildNavMesh();
    }

}
