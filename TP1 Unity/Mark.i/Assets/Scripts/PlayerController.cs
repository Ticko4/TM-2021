using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   
    private Rigidbody2D rb;
    private Animator anim;
    private enum State {idle, running,jumping,falling,hurt }
    private State state = State.idle;
    private Collider2D coll;
   
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpAfterKillForce = 5f;
   
    [SerializeField] private float dmgForce = 10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footStep;
    [SerializeField] private AudioSource playerHurt;
    [SerializeField] private int playerDmg = 1;
  
    private int damage = 1;
    private bool invincible = false;
    private int playerMaxHealth;
    private float startTime;
    private float holdTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        footStep =  GetComponent<AudioSource>();
        PlayerUI.playerUI.healthAmount.text = PlayerUI.playerUI.health.ToString();
        playerMaxHealth = PlayerUI.playerUI.health;
    }

    private void Update()
    {
        //movimento do personagem
        if(state != State.hurt)
        {
            movement();
        }

        //animações do personagem
        velocitySate();
        anim.SetInteger("state", (int)state);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyTree")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(state == State.falling)
            {
                enemy.JumpedOn();
                jump(jumpAfterKillForce);
            }
            else
            {
                if (!invincible)
                {
                    invincible = true;
                    playerHurt.Play();
                    state = State.hurt;
                    //vida do jogador
                    handleHP(damage);
                    if (collision.gameObject.transform.position.x > transform.position.x)
                    {
                        //Inimigo á direita (dano e mover esquerda)
                        rb.velocity = new Vector2(-dmgForce, rb.velocity.y);
                    }
                    else
                    {
                        //Inimigo á esquerda (dano e mover direita)
                        rb.velocity = new Vector2(dmgForce, rb.velocity.y);
                    }
                    Invoke("resetInvulnerability", 2);
                }

                   
            }
          
        }
        else if(collision.gameObject.tag == "Boss")
        {
            if (state == State.falling)
            {
                collision.gameObject.GetComponent<BossHealth>().TakeDamage(playerDmg);
            }
        }
    }

    public void handleHP(int damage)
    {
        PlayerUI.playerUI.health -= damage;
        PlayerUI.playerUI.healthAmount.text = PlayerUI.playerUI.health.ToString();
        if (PlayerUI.playerUI.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void EnemyDamage(int EnemyDamage, Collider2D collision)
    {
        if (!invincible && state != State.falling)
        {
            invincible = true;
            playerHurt.Play();
            state = State.hurt;
            //vida do jogador
            handleHP(EnemyDamage);
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                //Inimigo á direita (dano e mover esquerda)
                rb.velocity = new Vector2(-dmgForce, rb.velocity.y);
            }
            else
            {
                //Inimigo á esquerda (dano e mover direita)
                rb.velocity = new Vector2(dmgForce, rb.velocity.y);
            }
            Invoke("resetInvulnerability", 2);
        }  
    }

    void resetInvulnerability()
    {
        invincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            PlayerUI.playerUI.cherries += 1;
            PlayerUI.playerUI.cherryText.text = PlayerUI.playerUI.cherries.ToString();
        }

        if (!invincible)
        {
            if (collision.CompareTag("Projectile"))
            {
                invincible = true;
                state = State.hurt;
                //vida do jogador
                handleHP(damage);
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    //Inimigo á direita (dano e mover esquerda)
                    rb.velocity = new Vector2(-dmgForce, rb.velocity.y);
                }
                else
                {
                    //Inimigo á esquerda (dano e mover direita)
                    rb.velocity = new Vector2(dmgForce, rb.velocity.y);
                }
            }
            Invoke("resetInvulnerability", 2);
        }
         
    }

    private void movement()
    {
        float hdirection = Input.GetAxis("Horizontal");
        if (hdirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        else if (hdirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
           
            //salta com a tecla pra jump
            //jump(jumpForce);
            //Personagem ja n "agarra" as paredes
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.3f, ground);
            print(hit.collider.tag);
            if (hit.collider != null)
            {
                if(hit.collider.tag == "Foreground" && state == State.jumping)
                {
                    state = State.falling;
                }
                else
                {
                    jump(jumpForce);
                }
              
            }
            else
            {
                state = State.falling;
            }
              
        
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            startTime = Time.time;
            if (startTime + holdTime >= Time.time)
            {
                if(PlayerUI.playerUI.health < playerMaxHealth && PlayerUI.playerUI.cherries > 0)
                {
                    PlayerUI.playerUI.health++;
                    PlayerUI.playerUI.healthAmount.text = PlayerUI.playerUI.health.ToString();
                    PlayerUI.playerUI.cherries--;
                    PlayerUI.playerUI.cherryText.text = PlayerUI.playerUI.cherries.ToString();
                }
            }
               
        }
    }

    private void jump(float jumpVal)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVal);
        state = State.jumping;
    }

    private void velocitySate()
    {
        if (state == State.running && rb.velocity.y < -0.1f && !coll.IsTouchingLayers(ground))
        {
            //o player está em queda livre
            state = State.falling;
        }
        else if (state == State.jumping)
        {
            //Personagem a saltar
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
         
            if (rb.velocity.y < -0.1f)
            {
                state = State.falling;
            }else 
            //caso player esteja a saltar, so deixa repetir o salto caso esteja a tocar no chao
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
            
        }
        else if (state == State.hurt)
        {
            //caso tenha recebido dano desativa input e aplica força no corpo do jogador
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Personagem está a mover
            state = State.running;
        }
        else
        {
            //Personagem parada
            state = State.idle;
        }
    }

    private void FootStep()
    {
        footStep.Play();
    }
}
