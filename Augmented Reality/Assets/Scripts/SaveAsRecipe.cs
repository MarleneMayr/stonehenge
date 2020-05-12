using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using Bricks;

public class SaveAsRecipe
{

    #if UNITY_EDITOR

    public static Recipe CreateRecipeAsset(bool redirectToAsset = true)
    {
        Recipe asset = ScriptableObject.CreateInstance<Recipe>();
        string uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/Recipes/recipe.asset");
        AssetDatabase.CreateAsset(asset, uniqueFileName);
        EditorUtility.SetDirty(asset);
        asset.ingredients = GetCurrentBricks();
        AssetDatabase.SaveAssets();

        if (redirectToAsset)
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
        return asset;
    }

    #endif

    private static RecipeBrick[] GetCurrentBricks()
    {
        PhysicsBrick[] physicsBricks = Object.FindObjectsOfType<PhysicsBrick>();
        RecipeBrick[] recipeBricks = new RecipeBrick[physicsBricks.Length];

        for (int i = 0; i < physicsBricks.Length; i++)
        {
            PhysicsBrick pBrick = physicsBricks[i];
            BrickUtility.AlignBrick(pBrick);
            RecipeBrick brick = new RecipeBrick(pBrick.GetID(), pBrick.GetVoxels());

            recipeBricks[i] = brick;
        }
        Debug.Log(recipeBricks.Length + " bricks saved to new recipe.");
        return recipeBricks;
    }
}
