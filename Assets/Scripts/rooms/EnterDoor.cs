using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    public HeroRoom room;
    public Vector2Int side;

    private bool stayDoor;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && stayDoor)
        {
            room.MovingRoom(side);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            stayDoor= true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            stayDoor = false;
        }

    }
}
