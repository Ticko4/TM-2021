using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    private Animator anim;
    [SerializeField] private AudioSource explodeSound;
    private bool exploding = false;
    private Rigidbody2D rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        var relativePos = player.position - transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg + 180;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        coll = GetComponent<Collider2D>();
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if ((transform.position.x == target.x && transform.position.y == target.y || coll.IsTouchingLayers(ground)) && !exploding)
        {
            rb.velocity = Vector2.zero; rb.angularVelocity = 0f;
            exploding = true;
            explodeSound.Play();
            anim.SetBool("Explode", true);
        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector2.zero; rb.angularVelocity = 0f;
        explodeSound.Play();
        anim.SetBool("Explode", true);
    }
  
}
