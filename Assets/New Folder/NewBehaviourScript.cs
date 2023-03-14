using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;

    public float playerSpeed = 2;
    public float jumpForce = 2;
    public bool isGrounded;
    public LayerMask groundLayerMask;
    public Transform respawnPoint;
    public float raycastLength;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, y: jumpForce);
        }
        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if(horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal >0)
        {
            spriteRenderer.flipX = false;
        }
        isGrounded = (bool)Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * raycastLength, Color.green);
        // To animate jumping
        anim.SetBool("isGrounded", isGrounded);
       

    }
    // To collect coins and respawn
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
            Destroy(other.gameObject);

        if (other.tag == "Respawn")
        {
            Respawn();
        }
    }
    


    void Respawn()
    {
        transform.position = respawnPoint.position;
    }

}

