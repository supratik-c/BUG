using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public enum OpeningDirection {Bottom, Top, Left, Right }

	public OpeningDirection _OpeningDirection;

	public RoomLists _RoomLists;

	private int roomIndex = 0;

	public bool Spawned;

	private void Awake()
	{
		_RoomLists = FindObjectOfType<RoomLists>();
	}
	public void Spawn()
	{
		if (_RoomLists.SpawnedRooms == _RoomLists.MaxRoomsToSpawn || Spawned || HitSomething()) 
		{
			Destroy(gameObject);
			return;
		}
		GameObject spawnedRoom = null;
		switch (_OpeningDirection) 
		{
			case OpeningDirection.Top:
				roomIndex = Random.Range(0, _RoomLists.BottomRooms.Count);
				spawnedRoom = Instantiate(_RoomLists.BottomRooms[roomIndex]);
				break;
			case OpeningDirection.Bottom:
				roomIndex = Random.Range(0, _RoomLists.TopRooms.Count);
				spawnedRoom = Instantiate(_RoomLists.TopRooms[roomIndex], transform.position, Quaternion.identity);
				break;
			case OpeningDirection.Right:
				roomIndex = Random.Range(0, _RoomLists.LeftRooms.Count);
				spawnedRoom =  Instantiate(_RoomLists.LeftRooms[roomIndex], transform.position, Quaternion.identity);
				break;
			case OpeningDirection.Left:
				roomIndex = Random.Range(0, _RoomLists.RightRooms.Count);
				spawnedRoom = Instantiate(_RoomLists.RightRooms[roomIndex], transform.position, Quaternion.identity);
				break;
		}
		Spawned = true;
		_RoomLists.SpawnedRooms += 1;
		
		spawnedRoom.transform.position = transform.position;

		//spawnedRoom.name = spawnedRoom.name + " Spawned by " + transform.parent.parent.name;
		_RoomLists.RoomPoses.Add(spawnedRoom.transform.position);
	}

	public bool HitSomething() 
	{
		if (_RoomLists.RoomPoses.Contains(transform.position)) 
		{
			return true;
		}
		return false;
	}
}
