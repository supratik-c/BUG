using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;

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

    public List<Vector3> RoomPoses = new List<Vector3>();

    private int EnemyCount;

    public GameObject enemy;

	private void Awake()
	{
        SpawnedRooms = 0;
        RoomPoses.Add(Vector3.zero);
        EnemyCount = MaxRoomsToSpawn / 3;
	}

	private void Start()
	{
        StartCoroutine(spawnRooms());
	}

    private void FindClose() 
    {
        foreach (Vector3 pos in RoomPoses) 
        {
            foreach (Vector3 postocheck in RoomPoses) 
            {
                if (postocheck != pos && Vector3.Distance(pos,postocheck) < 1) 
                {
                    UnityEngine.Debug.Log($"Found 2 poses close together {pos} and {postocheck}");
                }
            }
        }
    }

    private void SpawnEnemy() 
    {
        List<Vector3> validRooms = new List<Vector3>();
        for (int i = 0;i < RoomPoses.Count;i++)
        {
            if (Vector3.Distance(RoomPoses[i],Vector3.zero) > 30) 
            {
                validRooms.Add(RoomPoses[i]);
            }
        } 




        for (int i = 0; i < EnemyCount; i++) 
        {
            Vector3 spawnPoint = ((Random.insideUnitSphere * 4) + validRooms[Random.Range(0, validRooms.Count)] ) /* 4*/;

            Instantiate(enemy, spawnPoint,Quaternion.identity);
        }
    }

	IEnumerator spawnRooms() 
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();

        while (SpawnedRooms < MaxRoomsToSpawn || spawningDone) 
        {
            List<RoomSpawner> ActiveSpawnPoints = new List<RoomSpawner>();

            ActiveSpawnPoints.AddRange(FindObjectsOfType<RoomSpawner>());

            if (ActiveSpawnPoints.Count == 0) 
            {
                spawningDone = true;

                UnityEngine.Debug.Log("Found No Spawn Points");
                _NavMeshSurface.BuildNavMesh();
                yield break;
            }

            foreach (RoomSpawner spawner in ActiveSpawnPoints) 
            {
                if (spawner != null && !spawner.Spawned) 
                {
                    spawner.Spawn();
                    
                    Destroy(spawner.gameObject);
                }
                yield return new WaitForEndOfFrame();

            }
            yield return new WaitForEndOfFrame();
        }
        _NavMeshSurface.BuildNavMesh();
        FindClose();
        sw.Stop();
        UnityEngine.Debug.Log($"Time taken = {sw.Elapsed}");
        SpawnEnemy();
    }

}
