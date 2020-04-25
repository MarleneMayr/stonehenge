using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class PickUp : MonoBehaviour
{
    private GameObject touched;
    private FixedJoint joint;
    private Material mat;

    private void Start()
    {
        joint = GetComponent<FixedJoint>();
        mat = GetComponent<MeshRenderer>().material;

        mat.color = Color.white;
    }

    private void OnCollisionEnter(Collision collision)
    {
        touched = collision.gameObject;
        mat.color = Color.green;
        collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (touched == collision.gameObject)
        {
            touched = null;
        }
        mat.color = Color.white;
        collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void AttachGameObject()
    {
        var rb = touched?.GetComponent<Rigidbody>();
        if (rb == null) return;
        joint.connectedBody = rb;
        mat.color = Color.yellow;
    }

    public void DetachGameObject()
    {
        joint.connectedBody = null;
        mat.color = Color.white;
    }
}
