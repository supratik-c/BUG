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

    //public GameObject enemy;

    public List<GameObject> Enemys = new List<GameObject>();

    private Transform roomParent;
    private Transform enemyParent;

    public GameManager GM;

	private void Awake()
	{
        SpawnedRooms = 0;
        RoomPoses.Add(Vector3.zero);
        
        roomParent = new GameObject().transform;
        roomParent.name = "RoomParent";
        enemyParent = new GameObject().transform;
        enemyParent.name = "enemyParent";
        GM = FindObjectOfType<GameManager>();

        int numRooms = PlayerPrefs.GetInt("RoomMod");
        UnityEngine.Debug.Log(numRooms);
        if (numRooms < 50)
        {
            MaxRoomsToSpawn = 50;
        }
        else 
        {
            MaxRoomsToSpawn = numRooms;
        }
        EnemyCount = MaxRoomsToSpawn / 3;
        UnityEngine.Debug.Log($"max rooms to spawn = {MaxRoomsToSpawn}");
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

        if (validRooms.Count == 0) 
        {
            validRooms = RoomPoses;
        }

        for (int i = 0; i < EnemyCount; i++) 
        {
            Vector3 spawnPoint = (Random.insideUnitSphere * 4) + validRooms[Random.Range(0, validRooms.Count)];

            GameObject enemy = Enemys[Random.Range(0, Enemys.Count)];

            Instantiate(enemy, spawnPoint,Quaternion.identity,enemyParent);
            enemy.GetComponent<AgentNavigation>().Spawner = this;
            GM.TotalEnemy += 1;
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
                SpawnedRooms = MaxRoomsToSpawn;
                UnityEngine.Debug.Log("Found No Spawn Points");
                _NavMeshSurface.BuildNavMesh();
                FindClose();
                sw.Stop();
                UnityEngine.Debug.Log($"Time taken = {sw.Elapsed}");
                SpawnEnemy();
                GM.Init();
                yield break;
            }

            foreach (RoomSpawner spawner in ActiveSpawnPoints) 
            {
                if (spawner != null && !spawner.Spawned) 
                {
                    spawner.Spawn(roomParent);
                    
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
        GM.Init();
    }

}
