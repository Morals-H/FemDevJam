using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAI : MonoBehaviour
{
    private NavMeshAgent nav;
    private bool skip;

    public Transform target;
    public float minDistance;

    private int timeLeft;
    public int timeBeforeWonder;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    public void FixedUpdate()
    {
        if (skip)
        {
            skip = false;
            return;
        }
        else skip = true;

        if (target)
        {
            //if player is outside of prefered range
            if (Mathf.Abs(Vector2.Distance(transform.position, target.position)) >= minDistance)
            {
                nav.SetDestination(target.position);
            }
            //if player is to close
            else if (Mathf.Abs(Vector2.Distance(transform.position, target.position)) < minDistance * 0.75f)
            {
                //getting the negative position of the target
                Vector2 negTarget = -transform.InverseTransformPoint(target.position);
                //figuring the the humanoid's movement
                Vector2 inverseForce = new Vector2(transform.position.x + negTarget.x, transform.position.y + negTarget.y);

                //movement
                nav.SetDestination(inverseForce);
            }
            else
            {
                //stopping the character
                nav.SetDestination(transform.position);
            }
        }

        if (target) return;

        if (timeLeft == 0)
        {
            //making a random time to move to reduce stagnation
            float randTime = timeBeforeWonder * 0.5f;
            randTime = Random.Range(-randTime, randTime);
            timeLeft = Mathf.RoundToInt(timeBeforeWonder + randTime);
            Vector3 wonderTarget = transform.position + (Random.insideUnitSphere * Random.Range(-10, 10));

            //checking if reachable
            RaycastHit2D hit = Physics2D.Linecast(transform.position, wonderTarget);
            if (hit.collider) wonderTarget = hit.point;

            nav.SetDestination(wonderTarget);
        }
        else if (timeLeft > 0)
        {
            timeLeft--;
        }
    }
}