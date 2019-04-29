using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public float jumpPower;
    public float speed;
    public float maxSpeed;
    [Range(0,1)]
    public float braking;

    private Rigidbody2D body;

    public event Action Jump;
    
    public enum MoveState { Grounded, Airborne }
    public MoveState mState;
    
    public List<Animator> animators = new List<Animator>();
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    
    private float inputX
    {
        get { return Input.GetAxis("Horizontal"); }
    }
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Jump += AttemptToJump;

        animators.AddRange(GetComponentsInChildren<Animator>());
        sprites.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump?.Invoke();
        }

        if (body.velocity.x > 0.1f )
            foreach (SpriteRenderer s in sprites)
            {
                s.flipX = false;
            }

        if (body.velocity.x < -0.1f )
            foreach (SpriteRenderer s in sprites)
            {
                s.flipX = true;
            }

        if (body.velocity.x > 0.1f || body.velocity.x < -0.1f) 
        {
            foreach (Animator a in animators)
            {
                a.SetFloat("Speed", 1);
            }
        }
        else
        {
            foreach (Animator a in animators)
            {
                a.SetFloat("Speed", 0);
            }
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(Mathf.Lerp(body.velocity.x, 0, braking * 2), body.velocity.y);
            return;
        }
        
        Vector2 force = new Vector2(inputX * speed * 100 * Time.deltaTime, 0);
        
        body.AddForce(force);
        
        if (inputX <= 0.1f && inputX >= -0.1f)
            body.velocity = new Vector2(Mathf.Lerp(body.velocity.x, 0, braking), body.velocity.y);
        
        body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -maxSpeed, maxSpeed), body.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        foreach (ContactPoint2D hit in other.contacts)
        {
            // Debug.Log(hit.normal);
            Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);

            if (hit.normal.y > 0.5f) // if on a semi flat ground, we're grounded
                mState = MoveState.Grounded;
            else
                mState = MoveState.Airborne;
        }
    }

    public void AttemptToJump()
    {
        
        if (mState != MoveState.Grounded) return;
        mState = MoveState.Airborne;
        
        Debug.Log("JUMPING");
        
        foreach (Animator a in animators)
        {
            a.SetTrigger("Jump");
        }
        
        Vector2 jumpForce = new Vector2(0, jumpPower);
        body.AddForce(jumpForce  * 4);
    }

    private void OnDestroy()
    {
        Jump -= AttemptToJump;
    }
}
