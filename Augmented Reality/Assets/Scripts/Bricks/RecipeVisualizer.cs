using Bricks;
using System;
using UnityEngine;

public class RecipeVisualizer : MonoBehaviour
{
    public static float SCALE = 0.5f;
    public static Vector3 SCALE3D = new Vector3(SCALE, SCALE, SCALE);

    public RecipeBrickVisualization brickPrefab;
    [SerializeField] private ColorPalette colorPalette;

    public void ShowRecipe(Recipe rec)
    {
        ClearBricks();
        foreach (var ingredient in rec.ingredients)
        {
            var visualization = Instantiate(brickPrefab, transform);
            InitBrick(visualization, ingredient);
        }
    }

    private void ClearBricks()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void InitBrick(RecipeBrickVisualization vis, RecipeBrick recBrick)
    {
        vis.SetColor(colorPalette.Colors[recBrick.GetID()]);

        Vector3 pos = transform.position + recBrick.GetVoxels()[1].getCenter() * SCALE;
        Quaternion rot = Quaternion.LookRotation(GetMajorAxis(recBrick.GetVoxels()));
        vis.transform.SetPositionAndRotation(pos, rot);
    }

    public Vector3 GetMajorAxis(Voxel[] voxels)
    {
        Vector3 direction = voxels[1].coordinates - voxels[2].coordinates; // get the delta of all axes
        Vector3 directionAbs = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), Mathf.Abs(direction.z));

        if (directionAbs.x > directionAbs.y)
        {
            if (directionAbs.x > directionAbs.z)
            {
                return new Vector3(Math.Sign(direction.x), 0, 0);
            }
            else
            {
                return new Vector3(0, 0, Math.Sign(direction.z));
            }
        }
        else if (directionAbs.y > directionAbs.z)
        {
            return new Vector3(0, Math.Sign(direction.y), 0);
        }
        else
        {
            return new Vector3(0, 0, Math.Sign(direction.z));
        }
    }
}
