using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool isGrounded = true;
    public float distance = 0.3f;
    public LayerMask collisionLayerMask;
    public float offset = 0.2f;

    private void FixedUpdate()
    {
        Ray l_Ray1 = new Ray(transform.position + new Vector3 (offset, 0, 0), Vector3.down);
        Ray l_Ray2 = new Ray(transform.position + new Vector3(- offset, 0, 0), Vector3.down);
        Ray l_Ray3 = new Ray(transform.position + new Vector3(0, 0, offset), Vector3.down);
        Ray l_Ray4 = new Ray(transform.position + new Vector3(0, 0, - offset), Vector3.down);
        if (Physics.Raycast(l_Ray1, distance + 0.01f, collisionLayerMask.value) ||
            Physics.Raycast(l_Ray2, distance + 0.01f, collisionLayerMask.value) ||
            Physics.Raycast(l_Ray3, distance + 0.01f, collisionLayerMask.value) ||
            Physics.Raycast(l_Ray4, distance + 0.01f, collisionLayerMask.value))
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
