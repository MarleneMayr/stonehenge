using System.Data.Common;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public enum ID
    {
        red,
        blue,
        yellow,
        green,
        white
    }

    [SerializeField] private ID identifier;
    protected Voxel[] occupiedVoxels;

    public Brick(ID id = ID.white, Voxel[] voxels = null)
    {
        identifier = id;
        occupiedVoxels = voxels;
    }

    public virtual Voxel[] getOccupiedVoxels()
    {
        return occupiedVoxels;
    }

    public bool match(Brick brick)
    {
        if (identifier == brick.identifier)
        {
            foreach (var voxel in brick.occupiedVoxels)
            {
                // if at least one voxel does not match, the whole brick does not match
                if (matchOccupiedVoxel(voxel) == false) return false;
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
    private bool matchOccupiedVoxel(Voxel other)
    {
        foreach (var v in occupiedVoxels)
        {
            if (other.Equals(v)) return true;
        }
        return false;
    }
}
