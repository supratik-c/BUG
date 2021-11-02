using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //public List<GameObject> Rooms = new List<GameObject>();

    //public Dictionary<GameObject, GameObject[]> RoomList = new Dictionary<GameObject, GameObject[]>();

    public List<Room> Rooms = new List<Room>();

    public int RoomCount;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateLevel()
    {
        Room previousRoom = null;

        for (int i = 0; i < RoomCount; i++) 
        {
            Room room = null;
            if (previousRoom == null)
            {
                room = Rooms[0];
            }
            else 
            {
                GameObject RandCompatibleRoom = previousRoom.CompatibleRooms[Random.Range(0, previousRoom.CompatibleRooms.Count)];

                foreach (Room _room in Rooms) 
                {
                    if (_room.Model == RandCompatibleRoom) 
                    {
                        room = _room;
                        break;
                    }
                }
            }
            GameObject ActiveRoomObject = Instantiate(room.Model);
            ActiveRoomObject.name = $"Room_{i}";
        }



    }

    [System.Serializable]
    public class Room
    {
        public GameObject Model;
        public List<GameObject> CompatibleRooms = new List<GameObject>();
    }
}
