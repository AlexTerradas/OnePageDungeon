using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isGrounded = true;
    public float distance = 0.3f;
    public LayerMask collisionLayerMask;

    private void FixedUpdate()
    {
        Ray l_Ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(l_Ray, distance + 0.01f, collisionLayerMask.value))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    print("enter");
    //    if (other.CompareTag("Floor"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    print("exit");
    //    if (other.CompareTag("Floor"))
    //    {
    //        isGrounded = false;
    //    }
    //}
}
