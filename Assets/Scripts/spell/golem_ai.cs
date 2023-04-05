using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class golem_ai : MonoBehaviour
{
    public GameObject spell;
    public float maxSpeed;
    public Animator anim;
    public LayerMask whatIsPlayer;
    public float checkRadius;
    public float attackRadius;
    public GameObject firePoint;

    Vector3 moveVector;
    Rigidbody2D rb;
    bool isInMove;
    bool isInAttack;
    bool isAttack = false;
    GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        isInMove = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttack = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);
        moveVector = player.transform.position - transform.position;
        moveVector.Normalize();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero;
        Vector2 lookDir = player.transform.position - firePoint.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firePoint.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (isInMove && !isInAttack && !isAttack)
        {
            rb.MovePosition(transform.position + ((Vector3)moveVector * maxSpeed * Time.deltaTime));
        }
        else if (isInMove && isInAttack && !isAttack)
        {
            StartCoroutine(attack());
        }

    }

    IEnumerator attack()
    {
        isAttack = true;

        yield return new WaitForSeconds(1f);
        GameObject pojectile = Instantiate(spell, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = pojectile.GetComponent<Rigidbody2D>();
        enemy_spell spl = pojectile.GetComponent<enemy_spell>();
        Vector3 fp = firePoint.transform.up;
        rb.AddForce(fp * spl.speed, ForceMode2D.Impulse);

        isAttack = false;
    }
}

