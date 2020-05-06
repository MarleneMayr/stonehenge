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

    public virtual Voxel[] getOccupiedVoxels()
    {
        return occupiedVoxels;
    }
}
