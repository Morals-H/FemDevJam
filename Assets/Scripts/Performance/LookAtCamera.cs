using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private bool skip;
    private Transform mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //activating every other fixed update
        if (skip)
        {
            skip = false;
            return;
        }
        else skip = true;

        //preparing to view camera
        Vector3 lookVec = mainCamera.position;
        lookVec.y = 0;

        //looking at camera
        transform.LookAt(lookVec);

    }
}
