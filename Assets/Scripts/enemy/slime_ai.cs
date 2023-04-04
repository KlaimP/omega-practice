using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class slime_ai : MonoBehaviour
{
    public float maxSpeed;

    public GameObject player;

    public float checkRadius;
    public LayerMask whatIsPlayer;
    
    public Animator anim;

    Vector2 moveVector;

    Rigidbody2D rb;

    bool isInChaseRange;

    bool isAttack = false;

    bool coroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector2.zero;
        moveVector = player.transform.position - transform.position;
        moveVector.Normalize();

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
    }
    private void FixedUpdate()
    {

        anim.SetFloat("Horizontal", moveVector.x);
        anim.SetFloat("Vertical", moveVector.y);

        if (isInChaseRange && !coroutine)
        {
            StartCoroutine(stopAttack());
        }
            //rb.MovePosition(transform.position + ((Vector3)moveVector * maxSpeed * Time.deltaTime));
        if(isAttack)
        {
            rb.MovePosition(transform.position + ((Vector3)moveVector * maxSpeed * Time.deltaTime));
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = transform.position;
        Handles.color = Color.red;
        Handles.DrawWireDisc(origin, new Vector3(0, 0, 1), checkRadius);
    }

    IEnumerator stopAttack()
    {
        coroutine = true;

        yield return new WaitForSeconds(Random.Range(0,11)/10);
        anim.SetBool("Move", true);
        yield return new WaitForSeconds(2f);
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        anim.SetBool("Move", false);

        coroutine = false;
    }
}
