using UnityEngine;

public class Voxel
{
    public static float SCALE = 0.1f;
    public static Vector3 SCALE3D = new Vector3(SCALE, SCALE, SCALE);
    private static Vector3 centerOffset = new Vector3(0.5f, 0.5f, 0.5f);

    private Vector3Int coordinates;

    public Voxel(Vector3 worldPosition)
    {
        int x = ToVoxelspace(worldPosition.x);
        int y = ToVoxelspace(worldPosition.y);
        int z = ToVoxelspace(worldPosition.z);
        coordinates = new Vector3Int(x, y, z);
    }

    public Voxel(Vector3Int coordinates)
    {
        this.coordinates = coordinates;
    }

    private int ToVoxelspace(float value)
    {
        return Mathf.FloorToInt(value / SCALE);
    }

    public Vector3 getCenter()
    {
        return (coordinates + centerOffset) * SCALE;
    }

    public override bool Equals(System.Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            return coordinates == ((Voxel)obj).coordinates;
        }
    }

    public override string ToString()
    {
        return coordinates.ToString();
    }
}
