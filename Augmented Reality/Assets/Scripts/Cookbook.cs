using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
#endif

public class Cookbook : MonoBehaviour
{
    [SerializeField] private Recipe[] recipes;
    private static int recipeIndex = 0;

    public Recipe GetNext()
    {
        Recipe next = recipes[recipeIndex];
        recipeIndex++;
        if (recipeIndex >= recipes.Length) recipeIndex = 0;
        return next;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Cookbook))]
    [CanEditMultipleObjects]
    public class CookbookEditor : Editor
    {
        private string folderPath = "Assets/Recipes";

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(10);

            var cookbook = (Cookbook)target;
            if (GUILayout.Button("save and add current recipe"))
            {
                var recipe = SaveAsRecipe.CreateRecipeAsset(false);
                ArrayUtility.Add(ref cookbook.recipes, recipe);
                ApplyArrayChanges(cookbook, cookbook.recipes);
            }

            if (GUILayout.Button("load all recipes"))
            {
                string[] guids = AssetDatabase.FindAssets("t:Recipe", new[] { folderPath });
                List<Recipe> loadedRecipes = new List<Recipe>();
                foreach (string guid in guids)
                {
                    loadedRecipes.Add(AssetDatabase.LoadAssetAtPath<Recipe>(AssetDatabase.GUIDToAssetPath(guid)));
                }
                Debug.Log(loadedRecipes.Count + " recipes loaded.");
                cookbook.recipes = loadedRecipes.ToArray();

                ApplyArrayChanges(cookbook, cookbook.recipes);
            }

            if (GUILayout.Button("open recipe folder"))
            {
                EditorUtility.FocusProjectWindow();
                EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<DefaultAsset>(folderPath));
            }

            GUILayout.Space(20);
            if (GUILayout.Button("clear"))
            {
                cookbook.recipes = new Recipe[0];
                ApplyArrayChanges(cookbook, cookbook.recipes);
            }
        }

        private void ApplyArrayChanges(Cookbook cookbook, Recipe[] newArray)
        {
            cookbook.recipes = newArray;
            // apply overrides if prefab instance was edited
            if (PrefabUtility.IsPartOfAnyPrefab(cookbook))
            {
                PrefabUtility.ApplyPrefabInstance(cookbook.gameObject, InteractionMode.UserAction);
            }

            // apply changes if prefab was edited in prefab scene
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }
    }
#endif
}
