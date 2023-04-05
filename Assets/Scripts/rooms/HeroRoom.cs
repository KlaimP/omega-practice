using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    public new GameObject camera;

    [Space]
    [Header("Enemy spawns:")]
    public GameObject[] spawnPoint;
    public GameObject[] enemies;

    [Space]
    [Header("Open door sprite:")]
    public Sprite ODoorT;
    public Sprite ODoorOther;

    [Space]
    public bool enableMoved = false;
    public bool cleared = false;

    private GameObject player;
    private GameObject[] randomArray;
    private bool startCleared = false;
    private GameObject[] spawnedEnemies;
    private SpriteRenderer sprite;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        randomArray = spawnPoint.OrderBy(x => Random.Range(0, spawnPoint.Length)).ToArray();
    }

    private void Update()
    {
        if(!cleared && startCleared)
        {
            for(int i = 0;i<spawnedEnemies.Length; i++)
            {
                if (!spawnedEnemies[i].IsDestroyed())
                    return;
            }
            Debug.Log("Room is cleared");
            cleared= true;
            enableMoved = true;

            sprite = DoorT.GetComponent<SpriteRenderer>();
            sprite.sprite = ODoorT;
            sprite = DoorR.GetComponent<SpriteRenderer>();
            sprite.sprite = ODoorOther;
            sprite = DoorL.GetComponent<SpriteRenderer>();
            sprite.sprite = ODoorOther;
            sprite = DoorB.GetComponent<SpriteRenderer>();
            sprite.sprite = ODoorOther;
        } 
    }

    public void MovingRoom(Vector2Int side)
    {
        if (enableMoved)
        {
            Debug.Log("Moving Room");

            if (side == Vector2Int.up)
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            if (!cleared)
            {
                spawnedEnemies = new GameObject[Random.Range(2, (int)(spawnPoint.Length/1.5))];

                for (int i = 0; i < spawnedEnemies.Length; i++)
                {
                    spawnedEnemies[i] = Instantiate(enemies[Random.Range(0, enemies.Length)], randomArray[i].transform.position, randomArray[i].transform.rotation);
                }

                startCleared = true;
            }else 
            {
                enableMoved = true;

                sprite = DoorT.GetComponent<SpriteRenderer>();
                sprite.sprite = ODoorT;
                sprite = DoorR.GetComponent<SpriteRenderer>();
                sprite.sprite = ODoorOther;
                sprite = DoorL.GetComponent<SpriteRenderer>();
                sprite.sprite = ODoorOther;
                sprite = DoorB.GetComponent<SpriteRenderer>();
                sprite.sprite = ODoorOther;
            }

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
