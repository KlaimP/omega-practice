using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class slime_ai : MonoBehaviour
{
    public float maxSpeed;

    public float checkRadius;
    public LayerMask whatIsPlayer;
    
    public Animator anim;


    GameObject player;
    Stats playerStats;
    Vector2 moveVector;
    Rigidbody2D rb;

    bool isInChaseRange;
    bool isInAttackRange;

    bool isAttack = false;

    bool coroutine = false;

    enemyStat stats;

    bool isDmg = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<Stats>();
        stats = GetComponent<enemyStat>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector2.zero;
        moveVector = player.transform.position - transform.position;
        moveVector.Normalize();

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);

        if(isDmg && isInAttackRange)
        {
            isDmg= false;
            StartCoroutine(Attack());
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero;

        anim.SetFloat("Horizontal", moveVector.x);
        anim.SetFloat("Vertical", moveVector.y);

        if (isInChaseRange && !coroutine)
        {
            StartCoroutine(stopAttack());
        }
        if(isAttack)
        {
            rb.MovePosition(transform.position + ((Vector3)moveVector * maxSpeed * Time.deltaTime));
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Stats player = collision.gameObject.GetComponent<Stats>();
            //player.damage(stats.dmg);
            isInAttackRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInAttackRange = false;
        }
    }

    IEnumerator stopAttack()
    {
        coroutine = true;

        yield return new WaitForSeconds(Random.Range(0,20)/10);
        anim.SetBool("Move", true);
        yield return new WaitForSeconds(2f);
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        anim.SetBool("Move", false);

        coroutine = false;
    }
    IEnumerator Attack()
    {
        playerStats.damage(stats.dmg);
        yield return new WaitForSeconds(1f);
        isDmg = true;
    }
}
