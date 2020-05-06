using System;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;

public class PhysicsBrick : Brick
{
    [SerializeField] private Transform[] anchors;
    private static int anchorsSize = 3;

    private void OnValidate()
    {
        if (anchors.Length != anchorsSize)
        {
            Debug.LogWarning("Don't change the 'anchors' array size!");
            Array.Resize(ref anchors, anchorsSize);
        }
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

    public override Voxel[] getVoxels()
    {
        updateVoxels();
        return voxels;
    }

    public void updateVoxels()
    {
        voxels = new Voxel[anchorsSize];
        for (int i = 0; i < anchorsSize; i++)
        {
            voxels[i] = new Voxel(anchors[i].position);
        }
    }

    public bool match(Brick brick)
    {
        if (identifier == brick.id)
        {
            foreach (var voxel in brick.getVoxels())
            {
                // if at least one voxel does not match, the whole brick does not match
                if (matchVoxel(voxel) == false) return false;
            }
            // if the check passed, then all voxels match
            return true;
        }
        else
        {
            // return false if the identifier does not match
            return false;
        }
    }

    // assuming two center points can never be in the same voxel
    private bool matchVoxel(Voxel other)
    {
        foreach (var v in voxels)
        {
            if (other.Equals(v)) return true;
        }
        return false;
    }

    // assuming that the center of the brick is always equal to the position of the center anchor
    public void Drop()
    {
        Voxel v = new Voxel(transform.position);
        transform.DOMove(v.getCenter(), 0.3f);
    }
}
