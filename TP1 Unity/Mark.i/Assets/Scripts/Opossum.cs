using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Opossum : Enemy
{
    [SerializeField] private bool hasAi;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float speed = 1;
    private Collider2D coll;
    private bool facingLeft = true;
    public AIPath aiPath;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
    }

    protected void Update()
    {
        if (hasAi)
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            OpossumMove();
        }
      
    }

    private void OpossumMove()
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
