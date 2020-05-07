using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumpLenght = 1;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private AudioSource frogSound;

    private Collider2D coll;
    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Verifica se o sapo esta a saltar e muda a animação
        if(anim.GetBool("Jumping") == false && anim.GetBool("Falling"))
        {
            
        }
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }

        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            frogSound.Play();
            anim.SetBool("Falling", false);
        }
    }

    public void Move()
    {
        if (facingLeft)
        {
            //verifica se o sapo esta no limite do seu movimento para a esquerda
            if (transform.position.x > leftCap)
            {
                //certifica que está na direção correta
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                //se nao está no limite e está a tocar no chao sapo salta
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLenght, jumpHeight);
                    anim.SetBool("Jumping",true);
                }

            }
            else
            {
                if (coll.IsTouchingLayers(ground))
                {
                    facingLeft = false;
                }
            }
        }
        else
        {
            //verifica se o sapo esta no limite do seu movimento para a direita
            if (transform.position.x < rightCap)
            {
                //certifica que está na direção correta
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                //se nao está no limite e está a tocar no chao sapo salta
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLenght, jumpHeight);
                    anim.SetBool("Jumping", true);
                }

            }
            else
            {
                if (coll.IsTouchingLayers(ground))
                {
                    facingLeft = true;
                }
            }
        } 
    }
}
