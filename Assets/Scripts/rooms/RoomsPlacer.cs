using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsPlacer : MonoBehaviour
{
    public int CountRooms = 12;
    public HeroRoom[] RoomPrefabs;
    public HeroRoom StartingRoom;
    public Transform parentTransform;

    public HeroRoom[,] spawnedRooms;
   

    private void Start()
    {
        spawnedRooms = new HeroRoom[11, 11];
        spawnedRooms[5, 5] = StartingRoom;


        for (int i = 0; i < CountRooms; i++)
        {
            PlaceOneRoom();
        }
        
    }

    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        HeroRoom newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)], parentTransform);
        Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
        newRoom.transform.position = new Vector3(position.x - 5, position.y - 5, 0) * 25f;
        ConnectToSomething(newRoom,position);
        spawnedRooms[position.x, position.y] = newRoom;
    }
    
    private void ConnectToSomething(HeroRoom room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorT != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorB != null) neighbours.Add(Vector2Int.up);
        if (room.DoorB != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorT != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);


        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        HeroRoom selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            room.DoorT.SetActive(true);
            room.DoorTB = selectedRoom.DoorB;
            selectedRoom.DoorBT = room  .DoorT;
            selectedRoom.DoorB.SetActive(true);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorB.SetActive(true);
            room.DoorBT = selectedRoom.DoorT;
            selectedRoom.DoorTB = room.DoorB;
            selectedRoom.DoorT.SetActive(true);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(true);
            room.DoorRL = selectedRoom.DoorL;

            selectedRoom.DoorLR = room.DoorR;
            selectedRoom.DoorL.SetActive(true);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(true);
            room.DoorLR = selectedRoom.DoorR;

            selectedRoom.DoorRL = room.DoorL;
            selectedRoom.DoorR.SetActive(true);
        }

    }

}
