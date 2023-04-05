using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class enemy_spell : MonoBehaviour
{
    public float dmg;
    public float speed;

    private bool attack = false;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !attack)
        {
            attack = true;
            Stats playerStats = collision.gameObject.GetComponent<Stats>();
            Debug.Log("Hit player");
            playerStats.damage(dmg);
        }
        if (collision.tag == "wall" || collision.tag == "Player")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            //anim.SetBool("destroy", true);
            Destroy(gameObject, 0.4f);
        }
    }


}
