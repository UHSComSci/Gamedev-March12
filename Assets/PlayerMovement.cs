using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10;
    public float jump = 100;

    public Transform feetTransform;

    public LayerMask groundLayers;

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
        if (OnFloor())
        {
            Debug.Log("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }

    bool OnFloor()
    {
        Collider2D groundCheck =  Physics2D.OverlapCircle(feetTransform.position, 0.2f, groundLayers);
        if(groundCheck != null)
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

        // from -1 to 7
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);


        //float jump = Input.GetAxisRaw("Jump");
        
        float jump = Input.GetAxisRaw("Jump");

        if (jump > 0)
        {
            Jump();
        }
    }
}
