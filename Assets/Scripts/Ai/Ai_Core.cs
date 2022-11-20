using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Core : MonoBehaviour
{
    public Spawner mom;

    public Transform targets;

    public MeleeAI ai;

    public float Health;
    private bool stop;
    private void Start()
    {
        ai = GetComponent<MeleeAI>();
    }

    //checks objects that enter the sight
    //this is a rudimentary method, but its the least expensive 
    private void OnTriggerEnter(Collider other)
    {
        Damage dmg = other.GetComponent<Damage>();
        if (tag != other.tag && !dmg)
        {
            targets = other.transform;
            ai.target = targets;
        }
        else if (dmg)
        {
            if(Vector3.Distance(other.transform.position, transform.position) < 1.5f) HasDied();
        }

    }

    //when this object has died
    public void HasDied()
    {
        if (stop) return;
        stop = true;
        mom.spawned--;
        Destroy(this.gameObject);
    }
}
