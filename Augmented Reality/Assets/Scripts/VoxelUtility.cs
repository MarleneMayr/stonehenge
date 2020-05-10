using UnityEngine;
using UnityEditor;
using Bricks;
using System.Linq;

public static class VoxelUtility
{
    [MenuItem("Tools/Voxels/Align all bricks")]
    public static void AlignAllBricks()
    {
        PhysicsBrick[] physicsBricks = Object.FindObjectsOfType<PhysicsBrick>();
        foreach (var brick in physicsBricks)
        {
            AlignBrick(brick);
        }
    }

    [MenuItem("Tools/Voxels/Align selected bricks")]
    public static void AlignSelectedBricks()
    {
        PhysicsBrick[] physicsBricks = Selection.gameObjects.Select(go => go.GetComponent<PhysicsBrick>()).ToArray();
        if (physicsBricks.Length > 0)
        {
            foreach (var brick in physicsBricks)
            {
                AlignBrick(brick);
            }
        }
        else
        {
            Debug.LogWarning("No physicsBrick selected!");
        }
    }

    public static void AlignBrick(PhysicsBrick brick)
    {
        Vector3 pos = new Voxel(brick.transform.position).getCenter();
        Quaternion rot = Quaternion.LookRotation(brick.GetMajorAxis());
        brick.transform.SetPositionAndRotation(pos, rot);
    }
}
