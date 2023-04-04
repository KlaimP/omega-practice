using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell_ai : MonoBehaviour
{
    public float dmg;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            enemyStat enemy = collision.gameObject.GetComponent<enemyStat>();
            Debug.Log("Hit "+ enemy.gameObject.name);
            enemy.Hit(dmg);
        }
    }
}
