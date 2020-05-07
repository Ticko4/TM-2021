using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run_Enraged : StateMachineBehaviour
{
    public float speed = 3f;
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    public float attackRange = 3f;
    public float rangedAttack = 5f;
    [SerializeField] private float startTimeBtwShots = 10;
    private float timeBtwShots;
    private bool isReadyToShoot = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        timeBtwShots = startTimeBtwShots;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        if(animator.GetBool("RangedAttack") == false)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        } 
      

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        else if (Vector2.Distance(player.position, rb.position) > rangedAttack )
        {
            if (timeBtwShots <= 0)
            {
                animator.SetTrigger("RangedAttack");
                timeBtwShots = startTimeBtwShots;
              
            }
            else
            {
                timeBtwShots -= Time.fixedDeltaTime;
             
            }
          
        }

    }

   

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("RangedAttack");
    }
}
