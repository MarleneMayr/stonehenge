using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBrick : Brick
{
    public Transform[] anchors;
    private static int anchorsSize = 3;

    private void OnValidate()
    {
        if (anchors.Length != anchorsSize)
        {
            Debug.LogWarning("Don't change the 'anchors' array size!");
            Array.Resize(ref anchors, anchorsSize);
        }

        Debug.Log(transform.lossyScale); // get the global scale
    }

    private void Update()
    {
        if (transform.position.y < -1)
        {
            Destroy(gameObject);
            // TODO place back on top of playground instead
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the anchors' positions
        Gizmos.color = Color.yellow;
        foreach (var t in anchors)
        {
            Gizmos.DrawSphere(t.position, 0.005f);
        }

        // Draw the occupied voxels
        Gizmos.color = Color.yellow;
        foreach (var t in anchors)
        {
            Voxel v = new Voxel(t.position);
            Gizmos.DrawWireCube(v.getCenter(), Voxel.SCALE3D);
            // Debug.Log($"{t.gameObject.name}: {t.position.ToString("F3")}, {v.Coordinates}");
        }
    }

    public override Voxel[] getOccupiedVoxels()
    {
        occupiedVoxels = new Voxel[anchorsSize];
        for (int i = 0; i < anchorsSize; i++)
        {
            occupiedVoxels[i] = new Voxel(anchors[i].position);
        }
        return occupiedVoxels;
    }
}
