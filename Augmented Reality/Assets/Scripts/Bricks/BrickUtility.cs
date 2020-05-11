using UnityEngine;
using UnityEditor;
using Bricks;
using System.Linq;

public static class BrickUtility
{
    [MenuItem("Tools/Voxels/Align all bricks &a")] // access this command with alt+a
    public static void AlignAllBricks()
    {
        PhysicsBrick[] physicsBricks = Object.FindObjectsOfType<PhysicsBrick>();
        foreach (var brick in physicsBricks)
        {
            AlignBrick(brick);
        }
    }

    [MenuItem("Tools/Voxels/Align selected bricks &s")] // access this command with alt+s
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

    /// <summary>
    /// aligns the brick in voxel space, setting position and rotation to the nearest voxel values
    /// </summary>
    /// <param name="brick"></param>
    public static void AlignBrick(PhysicsBrick brick)
    {
        Vector3 pos = new Voxel(brick.transform.position).getCenter();
        Quaternion rot = Quaternion.LookRotation(brick.GetMajorAxis());
        brick.transform.SetPositionAndRotation(pos, rot);
    }

    /// <summary>
    /// places the brick in voxel space and snaps to voxel values
    /// </summary>
    /// <param name="brick"></param>
    /// <param name="position"></param>
    /// <param name="orientation"></param>
    public static void PlaceBrick(PhysicsBrick brick, Vector3 position, Vector3 orientation)
    {
        Vector3 pos = new Voxel(position).getCenter();
        Quaternion rot = Quaternion.LookRotation(orientation);
        brick.transform.SetPositionAndRotation(pos, rot);
    }

    /// <summary>
    /// places the brick in world space with regular float position values
    /// </summary>
    /// <param name="brick"></param>
    /// <param name="position"></param>
    /// <param name="orientation"></param>
    public static void PlaceBrickAbsolute(PhysicsBrick brick, Vector3 position, Vector3 orientation)
    {
        Quaternion rot = Quaternion.LookRotation(orientation);
        brick.transform.SetPositionAndRotation(position, rot);
    }
}
