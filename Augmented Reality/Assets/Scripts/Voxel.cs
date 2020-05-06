using UnityEngine;

public class Voxel
{
    public static float SCALE = 0.1f;
    public static Vector3 SCALE3D = new Vector3(SCALE, SCALE, SCALE);

    public Vector3Int Coordinates { get; private set; }
    private static Vector3 centerOffset = new Vector3(0.5f, 0.5f, 0.5f);

    public Voxel(Vector3 worldPosition)
    {
        int x = ToVoxelspace(worldPosition.x);
        int y = ToVoxelspace(worldPosition.y);
        int z = ToVoxelspace(worldPosition.z);
        Coordinates = new Vector3Int(x, y, z);
    }

    private int ToVoxelspace(float value)
    {
        return Mathf.FloorToInt(value / SCALE);
    }

    public Vector3 getCenter()
    {
        return (Coordinates + centerOffset) * SCALE;
    }

}
