using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    public HeroRoom room;
    public Vector2Int side;

    private bool stayDoor;
    private void Start()
    {

    }
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
            
            GameObject game = GameObject.Find("Press E");
            Animator anim = game.GetComponent<Animator>();

            anim.SetTrigger("enable");
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
