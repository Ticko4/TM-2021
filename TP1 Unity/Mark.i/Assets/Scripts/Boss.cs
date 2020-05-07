using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool facingRight = true;
    [SerializeField] private GameObject projectile;
    
    [SerializeField] private AudioSource shootingSound;
    [SerializeField] private AudioSource startSound;

    void Start()
    {
        startSound.Play();
    }

    public void LookAtPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) < 20)
        {

            if (player.position.x > transform.position.x && !facingRight)
                Flip();
            if (player.position.x < transform.position.x && facingRight)
                Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    public void Shoot()
    {
        shootingSound.Play();
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
