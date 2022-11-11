using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actions : MonoBehaviour
{

    //Physics
    public Rigidbody rig;
    public bool isGrounded = true;

    //Jumping
    public float jumpForce = 200f;
    private bool jumpToggle;
    private int jumpState;
    public int CoyoteTime;

    //Animations
    private Animator anim;
    private Transform cam;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    //stats
    public float curSpeed;
    public Vector2 MySpeed = new Vector2(6, 2);

    //Input
    private Vector2 movement;
    private bool isSprinting;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //sprint input
        if (Input.GetAxis("Sprint") > 0)
        {
            isSprinting = true;
        }
        else isSprinting = false;

        //Movement Input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        //setting animation
        if (movement == Vector2.zero)
        {
            anim.SetInteger("moveState", 0);

            curSpeed = 0;
        }
        else if (isSprinting)
        {
            anim.SetInteger("moveState", 2);

            curSpeed = MySpeed.x;
        }
        else
        {
            anim.SetInteger("moveState", 1);

            curSpeed = MySpeed.y;
        }

        //moving player relative of camera 
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0f);
            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * curSpeed;

            direction.y = rig.velocity.y;
            //moving with rigidbody
            rig.velocity = direction;
        }

        //getting jump input
        if (Input.GetAxis("Jump") > 0 && jumpToggle == false)
        {
            jumpToggle = true;

            if (jumpState == 0 && isGrounded || jumpState == 0 && CoyoteTime > 0)
            {
                rig.AddForce(transform.up * jumpForce, ForceMode.Impulse);

                jumpState = 1;
                return;
            }
            else if (jumpState == 0) jumpState = 1;

            if (jumpState == 1)
            {
                rig.AddForce(transform.up * jumpForce, ForceMode.Impulse);

                jumpState = 2;
                return;
            }

        }
        else if (Input.GetAxis("Jump") == 0) jumpToggle = false;

        //Logic
        if (!isGrounded && CoyoteTime > 0) CoyoteTime--;
    }

    private Vector3 m_Bounds = new Vector3(0.1275f, 1, 0.1275f);
    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.BoxCast(transform.position, m_Bounds, -transform.up, transform.rotation, 1))
        {
            return;
        }
        else
        {
            isGrounded = true;
            anim.SetBool("isGrounded", isGrounded);
            jumpState = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (Physics.BoxCast(transform.position, m_Bounds, -transform.up, transform.rotation, 1))
        {
            return;
        }
        else
        {
            isGrounded = false;
            anim.SetBool("isGrounded", isGrounded);
            CoyoteTime = 25;
        }
    }
}
