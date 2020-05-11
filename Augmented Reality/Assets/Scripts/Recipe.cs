using UnityEngine;
using Bricks;
using UnityEditor;

[System.Serializable]
public class Recipe : ScriptableObject
{
    public RecipeBrick[] ingredients;
}

[CustomEditor(typeof(Recipe))]
[CanEditMultipleObjects]
public class RecipeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Recipe recipe = (Recipe)target;

        foreach (var brick in recipe.ingredients)
        {
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.LabelField("ID:", brick.GetID().ToString());

                var ranges = GetRanges(brick.GetVoxels());
                EditorGUIUtility.labelWidth = 30;

                using (var h = new EditorGUILayout.HorizontalScope("Button"))
                {
                    EditorGUILayout.LabelField("x:", $"{ranges[0].from} to {ranges[0].to}");
                    EditorGUILayout.LabelField("y:", $"{ranges[1].from} to {ranges[1].to}");
                    EditorGUILayout.LabelField("z:", $"{ranges[2].from} to {ranges[2].to}");
                }
                EditorGUIUtility.labelWidth = 0;
            }
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); // horizontal line cheat
        }
    }

    private (int from, int to)[] GetRanges(Voxel[] voxels)
    {
        (int from, int to)[] ranges = new (int from, int to)[3];

        Voxel reference = voxels[0];
        for (int i = 1; i < voxels.Length; i++)
        {
            ranges[0] = (from: Mathf.Min(voxels[i].X, reference.X), to: Mathf.Max(voxels[i].X, reference.X));
            ranges[1] = (from: Mathf.Min(voxels[i].Y, reference.Y), to: Mathf.Max(voxels[i].Y, reference.Y));
            ranges[2] = (from: Mathf.Min(voxels[i].Z, reference.Z), to: Mathf.Max(voxels[i].Z, reference.Z));
        }
        return ranges;
    }
}