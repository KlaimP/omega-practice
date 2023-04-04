using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRoom : MonoBehaviour
{
    [Header("Enter doors:")]
    public GameObject DoorR;
    public GameObject DoorL;
    public GameObject DoorT;
    public GameObject DoorB;

    [Space]
    [Header("Exit doors:")]
    public GameObject DoorRL;
    public GameObject DoorLR;
    public GameObject DoorTB;
    public GameObject DoorBT;

    [Space]
    [Header("Virtual camera:")]
    public GameObject camera;


    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void MovingRoom(Vector2Int side)
    {
        Debug.Log("Moving Room");
        if(side == Vector2Int.up)
        {
            player.transform.position = DoorTB.transform.position;
        }
        else if (side == Vector2Int.right)
        {
            player.transform.position = DoorRL.transform.position;
        }
        else if (side == Vector2Int.down)
        {
            player.transform.position = DoorBT.transform.position;
        }
        else if (side == Vector2Int.left)
        {
            player.transform.position = DoorLR.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            camera.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            camera.SetActive(false);
        }
    }

}
