using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10;
    public float crouchSpeed = 5;
    public float jump = 100;

    public Transform groundCheck;
    public Transform ceilingCheck;

    public Collider2D disableOnCrouch;

    public LayerMask groundLayers;

    public Animator animator;

    bool lookingRight = true;
    bool crouching = false;
    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Jump()
    {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            animator.SetBool("jumping", true);
    }

    bool OnFloor()
    {
        Collider2D groundCheckCollider =  Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayers);
        if(groundCheckCollider != null)
        {
            return true;
        }
        return false;
    }

    bool CeilingCheck()
    {
        Collider2D ceilingCheckCollider = Physics2D.OverlapCircle(ceilingCheck.position, 0.2f, groundLayers);
        if (ceilingCheckCollider != null)
        {
            return true;
        }
        return false;
    }


    //Called once per physics frame
    private void FixedUpdate()
    {
        //A: go left : -X
        //D: go right: +X
        //Space: Jump: +Y

        /* Crouch */
        float crouch = Input.GetAxisRaw("Crouch");
        float move = Input.GetAxisRaw("Horizontal");
        if(crouch < 0 && !crouching)
        {
            //If crouching
            disableOnCrouch.enabled = false;
            crouching = true;
            animator.SetBool("crouching", true);
        }
        else if(crouch >= 0 && crouching)
        {
            //standing up
            if (!CeilingCheck())
            {
                disableOnCrouch.enabled = true;
                crouching = false;
                animator.SetBool("crouching", false);
            }
        }

        if (crouching)
            rb.velocity = new Vector2(move * crouchSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(move * speed, rb.velocity.y);

        /* Flipping the sprite when changing movement direction */
        bool newLookingRight = move > 0;
        if (move == 0) newLookingRight = lookingRight;
        if (newLookingRight != lookingRight)
            Flip();

        /* Jump */
        if(!grounded && OnFloor())
            animator.SetBool("jumping", false);
        grounded = OnFloor();
        float jump = Input.GetAxisRaw("Jump");
        if (jump > 0 && grounded)
            Jump();

        /* Animation */
        float actualSpeed = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("speed", actualSpeed);
    }

    void Flip()
    {
        lookingRight = !lookingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
