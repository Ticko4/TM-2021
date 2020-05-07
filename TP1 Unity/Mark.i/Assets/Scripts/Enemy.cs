using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    //protected AudioSource deathSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource hurtSound;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //deathSound = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
        //ao saltar no inimigo morre, dá o som e a animação e remove a velocidade do movimento do inimigo
        if (hurtSound)
        {
            hurtSound.Play();
        }
        deathSound.Play();
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }

}
