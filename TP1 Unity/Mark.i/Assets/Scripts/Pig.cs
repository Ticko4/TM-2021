using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Enemy
{
    [SerializeField] private bool idle = true;
    [SerializeField] private bool running;
    [SerializeField] private bool walk;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float speed = 1;
    [SerializeField] private float pigSoundTimer = 15;
    [SerializeField] private bool pigMuted = false;
    [SerializeField] private AudioSource pigSound;
  
    private Collider2D coll;
    private bool facingLeft = true;
    private float timePassed = 0;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        //anim.SetBool("Idle", idle);
        anim.SetBool("Walking", walk);
        anim.SetBool("Running", running);
      
    }

    protected void Update()
    {
        if (!idle)
        {
            PigMove();
        }

        if(!pigMuted)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= pigSoundTimer)
            {
                timePassed = 0;
                pigSound.Play();
            }
        }
       

    }

   
   


    private void PigMove()
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
                //se nao está no limite e está a tocar no chao
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-speed, 1);
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
            //verifica se o porco esta no limite do seu movimento para a direita
            if (transform.position.x < rightCap)
            {
              
                //certifica que está na direção correta
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                //se nao está no limite e está a tocar no chao
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(speed, 1);
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
