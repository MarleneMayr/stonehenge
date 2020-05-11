﻿using UnityEngine;

public class VoxelReference : MonoBehaviour
{
    public Voxel GetVoxel()
    {
        return new Voxel(transform.position);
    }
}
