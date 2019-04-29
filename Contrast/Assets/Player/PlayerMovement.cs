using System;
using System.Collections;
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
    
    private float inputX
    {
        get { return Input.GetAxis("Horizontal"); }
    }
    private float inputJump
    {
        get { return Input.GetAxis("Jump"); }
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Jump += AttemptToJump;
    }

    private void Update()
    {
        if (inputJump > 0.1f)
        {
            Jump?.Invoke();
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {

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
        
        Debug.Log("JUMPING");
        
        Vector2 jumpForce = new Vector2(0, jumpPower);
        body.AddForce(jumpForce);
        mState = MoveState.Airborne;
    }

    private void OnDestroy()
    {
        Jump -= AttemptToJump;
    }
}
