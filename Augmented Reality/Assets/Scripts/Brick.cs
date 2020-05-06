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

    [SerializeField] protected ID identifier;
    public ID id => identifier;

    protected Voxel[] voxels;

    public Brick(ID id = ID.white, Voxel[] voxels = null)
    {
        identifier = id;
        this.voxels = voxels;
    }

    public virtual Voxel[] getVoxels()
    {
        return voxels;
    }

    public override string ToString()
    {
        if (voxels == null) return "NaN";
        string result = "";
        foreach (var v in voxels)
        {
            result += $"{v.ToString()}";
        }
        return result;
    }
}
