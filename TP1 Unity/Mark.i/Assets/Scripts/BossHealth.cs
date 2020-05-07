using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    protected Animator anim;
    public int health = 15;
    public int secondStage = 7;
    public bool isInvulnerable = false;
    protected Rigidbody2D rb;
    protected BoxCollider2D coll;
    protected bool isDead = false;
    protected bool isEnrage = false;
    [SerializeField] private AudioSource enragedSound;
    [SerializeField] private AudioSource deathSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }else if (health <= secondStage)
        {
            if (!isEnrage)
            {
                enragedSound.Play();
                GetComponent<Animator>().SetBool("isEnraged", true);
                isEnrage = true;
            }
           
        }
    }

    void Die()
    {
        if (!isDead)
        {
            deathSound.Play();
            isDead = true;
            anim.SetTrigger("Death");
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Collider2D>().enabled = false;
            //anim.ResetTrigger("Death");
        }

    }
}
