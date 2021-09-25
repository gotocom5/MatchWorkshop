using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : StateMachine
{

    public new Rigidbody2D rigidbody;
    [SerializeField] private List<GameObject> groundCheck;
    [SerializeField] public float moveSpeed = 2.0f;
    [SerializeField] public float JumpSpeed = 2.0f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private GameObject groundedObject;
    [SerializeField] public bool jump;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D element in other.contacts)
            {
                if (element.normal.y > 0.25f)
                {
                    isGrounded = true;
                    groundedObject = other.gameObject;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == this.groundedObject)
        {
            groundedObject = null;
            isGrounded = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            SetState(new ReallyJump(this)); 
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            SetState(new MoveRight(this));
        }
        //角色水平移動
        //按住A鍵，判斷如果小於0，則向左開始移動
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            SetState(new MoveLeft(this));
        }
        else
        //角色水平移動
        //鬆開按鍵，判斷如果等於0，則停止移動
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
    }
}
