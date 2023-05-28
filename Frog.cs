using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask Ground;

    private Collider2D COLL;
    private Rigidbody2D RB;

    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        COLL = GetComponent<Collider2D>();
        RB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
        if(ANI.GetBool("IsJumping"))
        {
            if(RB.velocity.y < 0.1)
            {
                ANI.SetBool("IsFalling", true);
                ANI.SetBool("IsJumping", false);
            }
        }
        if(COLL.IsTouchingLayers(Ground) && ANI.GetBool("IsFalling"))
        {
            ANI.SetBool("IsFalling", false);
        }
    }

    private void EnMovement()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (COLL.IsTouchingLayers(Ground))
                {
                    RB.velocity = new Vector2(-jumpLength, jumpHeight);
                    ANI.SetBool("IsJumping", true);

                }
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (COLL.IsTouchingLayers(Ground))
                {
                    RB.velocity = new Vector2(jumpLength, jumpHeight);
                    ANI.SetBool("IsJumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

   


}
