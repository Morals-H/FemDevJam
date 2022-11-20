using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public SphereCollider col;
    public int life;

    private void FixedUpdate()
    {
        life--;

        if (life < 5) col.enabled = true;

        if (life < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
