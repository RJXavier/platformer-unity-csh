using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class P_Controller : MonoBehaviour
{
   
    public Rigidbody2D RB;
    public Animator ANI;
    private Collider2D COLL;
    
    private enum State { IsIdle, IsRunning, IsJumping, IsFalling, IsDamaged}
    private State state = State.IsIdle;
    
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float JumpForce = 10f;
    [SerializeField] private int Score = 0;
    [SerializeField] private TextMeshProUGUI ScoreCounter;
    [SerializeField] private float damageForce = 10f;
    [SerializeField] private AudioSource Pickup;
    [SerializeField] private AudioSource Step;
    [SerializeField] private AudioSource Hurt;
    [SerializeField] private AudioSource JumpS;
    [SerializeField] private int Life = 0;
    [SerializeField] private TextMeshProUGUI Lives;






   
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        ANI = GetComponent<Animator>();
        COLL = GetComponent<Collider2D>();
        Lives.text = Life.ToString();
       
    }

    
    private void Update()
    {
        if (state != State.IsDamaged)
        {
            Movement();
        }
        AnimationState();
        ANI.SetInteger("state", (int)state); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectible")
        {
            Pickup.Play();
            Destroy(collision.gameObject);
            Score += 1;
            ScoreCounter.text = Score.ToString();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (state == State.IsFalling)
            {
                enemy.Crushed();
                Jump();
            }
            else
            {
                state = State.IsDamaged;

                LifeHandler();

                if(collision.gameObject.transform.position.x > transform.position.x)
                {
                    RB.velocity = new Vector2(-damageForce, RB.velocity.y);
                }
                else
                {
                    RB.velocity = new Vector2(damageForce, RB.velocity.y);
                }
            }
            
        }
    }

    private void LifeHandler()
    {
        Life -= 1;
        Lives.text = Life.ToString();
        if (Life <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            RB.velocity = new Vector2(-Speed, RB.velocity.y); 
            transform.localScale = new Vector2(-1, 1);    

        }
        else if (hDirection > 0)
        {
            RB.velocity = new Vector2(Speed, RB.velocity.y); 
            transform.localScale = new Vector2(1, 1);

        }

        if (Input.GetButton("Jump") && COLL.IsTouchingLayers(Ground)) 
        {
            Jump();
        }
    }

    private void Jump()
    {
        RB.velocity = new Vector2(RB.velocity.x, JumpForce);
        state = State.IsJumping;
    }

    private void AnimationState()
    {
        if(state == State.IsJumping) 
        {
            if(RB.velocity.y < 0.1f)
            {
                state = State.IsFalling;
            }
        }
        else if(state == State.IsFalling) 
        {
            if(COLL.IsTouchingLayers(Ground))
            {
                state = State.IsIdle;
            }
        }
        else if(state == State.IsDamaged)
        {
            if(Mathf.Abs(RB.velocity.x) < 0.1f)
            {
                state = State.IsIdle;
            }
        }
        else if(Mathf.Abs(RB.velocity.x) > 2f) 
        {
            state = State.IsRunning;
           
        }
        else
        {
            state = State.IsIdle;
        }

    }

    private void AudStep()
    {
        Step.Play();
    }

    private void AudHurt()
    {
        Hurt.Play();
    }

    private void AudJump()
    {
        JumpS.Play();
    }
}
