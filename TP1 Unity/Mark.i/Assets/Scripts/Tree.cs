using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatDistance;
    [SerializeField] private Transform player;
    [SerializeField] private float distanceAgro;
    private enum State { idle, running, attacking }
    private State state = State.idle;

    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    [SerializeField] private GameObject projectile;

    private bool facingRight = false;
    [SerializeField] private AudioSource shootingSound;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        //Caso esteja a ataquar esperar que a animação acabe
        if(state != State.attacking || Vector2.Distance(transform.position, player.position) > distanceAgro)
        {
            //Caso esteja mais longe da distancia maxima
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                state = State.running;
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            //Caso esteja entre as distancias
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                state = State.idle;
                transform.position = this.transform.position;
            }
            //Caso esteja perto de mais
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                state = State.running;
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            //Timer para não disparar todos os frames
            if (timeBtwShots <= 0)
            {
                state = State.attacking;
                //para o inimigo nao disparar em todos os frames
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
                //state = State.idle;
            }
        }

        //Troca o inimigo de lado
        if (Vector3.Distance(player.position, transform.position) < 20)
        {

            if (player.position.x > transform.position.x && !facingRight) 
                Flip();
            if (player.position.x < transform.position.x && facingRight)
                Flip();
        }

        anim.SetInteger("state", (int)state);
    }

    public void Shoot()
    {
        shootingSound.Play();
        Instantiate(projectile, transform.position, Quaternion.identity);
    }

    public void ChangeState()
    {
        state = State.idle;
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }
}
