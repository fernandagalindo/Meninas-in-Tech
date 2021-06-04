using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public Transform groundCheck;
    private SpriteRenderer sprite;
    private Animator anim;

    private bool touchedWall;
    private bool facingRight;
    private int layerMask;

    public float speed;
    public int health;
    public bool isDead;

    public GameObject particleDead;
    public GameObject particleDamage;

    void Start()
    {
        touchedWall = false;
        facingRight = true;
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); 
        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        if (isDead)
            return;

        if (touchedWall)
        {
            touchedWall = false;
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            speed *= -1;
        }

        //touchedWall = Physics2D.Linecast(transform.position, groundCheck.position, layerMask);
    }

    private void FixedUpdate()
    {
        if (isDead)
            return;

        rb2D.velocity = new Vector2(speed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            DamageEnemy(); 
        }

        if (other.CompareTag("Ground"))
        {
            touchedWall = true;
        }
    }
    private void DamageEnemy()
    {
        health--;

        StartCoroutine(DamageEffect());

        if (health == 0)
        {
            isDead = true;

            //Instantiate(particleDead, gameObject.transform.position, gameObject.transform.rotation);

            rb2D.velocity = new Vector2(0f, 0f);
            anim.SetBool("isDead", true);
            StartCoroutine(DeadEnemy());
        }
        else
        {
            //Instantiate(particleDamage, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    IEnumerator DamageEffect()
    {
        float auxspeed = speed;
        speed *= -1;
        sprite.color = Color.red;
        rb2D.AddForce(new Vector2(0f, 200f));
        yield return new WaitForSeconds(.1f);
        speed *= -1;
        speed = auxspeed;
        sprite.color = Color.white;
    }

    IEnumerator DeadEnemy()
    {
        yield return new WaitForSeconds(1f);
        //Destroy(gameObject);
    }
}
