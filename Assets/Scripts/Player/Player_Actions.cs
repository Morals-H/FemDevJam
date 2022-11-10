using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player_Actions : MonoBehaviour
{
    //Input collection
    public static Player_Input PI;

    //Physics
    private Rigidbody rig;
    public bool isGrounded = true;

    //Jupming
    private float jumpForce = 200f;
    private bool jumpToggle;
    public int jumpState;
    public int CoyoteTime;

    //Animations
    private Animator anim;
    private Transform cam;

    //stats
    private float curSpeed;
    private Vector2 MySpeed = new Vector2(6, 3);

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        PI = GetComponent<Player_Input>();
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement Input
        if(PI.movement != Vector2.zero) Walk(PI.movement);

        //Jump Input
        if (PI.jump && !jumpToggle)
        {
            Jump();
            jumpToggle = true;
        } else if (!PI.jump) jumpToggle = false;

        //Logic
        if (!isGrounded && CoyoteTime > 0) CoyoteTime--;

    }

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private void Walk(Vector2 myInput)
    {
        //Input to Vector3
        Vector3 moveDir = new Vector3(myInput.x, 0, myInput.y);

        //Rotation
        float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //Movement
        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        curSpeed = Mathf.Lerp(curSpeed, (PI.sprint ? MySpeed.x : MySpeed.y), (PI.sprint ? 0.025f : 0.05f));

        moveDir *=  curSpeed * Time.deltaTime;
        rig.MovePosition(transform.position + moveDir);

    }

    //Jumping
    private void Jump()
    {
        //normal jump
        if (isGrounded || CoyoteTime > 0 && jumpState == 0)
        {
            rig.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpState = 1;
            return;
        }
        else if (jumpState == 0) jumpState = 1;
        
        //double jump
        if (jumpState == 1)
        {
            rig.velocity = Vector3.zero;
            rig.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            jumpState = 2;
        } 
        else return;
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
            Debug.Log("Grounded");
            isGrounded = true;
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
            Debug.Log("unGrounded");
            isGrounded = false;
            CoyoteTime = 25;
        }
    }
}
