using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public Vector2 movement;
    public bool jump;
    public bool sprint;


    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (Input.GetAxis("Jump") > 0)
        {
            jump = true;
        }
        else jump = false;

        if (Input.GetAxis("Sprint") > 0)
        {
            sprint = true;
        }
        else sprint = false;
    }
        
}
