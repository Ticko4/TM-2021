using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
   // [SerializeField] private bool idle = true;
   // [SerializeField] private bool running;
   // [SerializeField] private bool walk;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float speed = 1;
    private Collider2D coll;
    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        BatIdle();
      
    }

    protected void Update()
    {
       

        if (anim.GetBool("Fly"))
        {
            batMove();
        }
       
    }

    private void BatOut()
    {
        anim.SetBool("Out", true);
        anim.SetBool("In", false);
        anim.SetBool("Fly", false);
        anim.SetBool("Idle", false);
    }

    private void BatIn()
    {
        anim.SetBool("Out", false);
        anim.SetBool("In", true);
        anim.SetBool("Fly", false);
        anim.SetBool("Idle", false);
    }

    private void BatFly()
    {
        anim.SetBool("Out", false);
        anim.SetBool("In", false);
        anim.SetBool("Fly", true);
        anim.SetBool("Idle", false);
    }

    private void BatIdle()
    {
        anim.SetBool("Out", false);
        anim.SetBool("In", false);
        anim.SetBool("Fly", false);
        anim.SetBool("Idle", true);
        rb.velocity = Vector2.zero;
    }

    private void batMove()
    {


        if (facingLeft)
        {
            //verifica se o porco esta no limite do seu movimento para a esquerda
            if (transform.position.x > leftCap)
            {
                //certifica que está na direção correta
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                rb.velocity = new Vector2(-speed, 1);
                //se nao está no limite e está a tocar no chao
            }
            else
            {
               facingLeft = false;
            }
        }
        else
        {
            //verifica se o porco esta no limite do seu movimento para a direita
            if (transform.position.x < rightCap)
            {
                //certifica que está na direção correta
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                rb.velocity = new Vector2(speed, 1);
                //se nao está no limite e está a tocar no chao
             
            }
            else
            {
                facingLeft = true; 
            }
        }
    }
}
