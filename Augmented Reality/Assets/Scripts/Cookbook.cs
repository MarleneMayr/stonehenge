using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookbook : MonoBehaviour
{
    private Recipe[] recipes;
    private static int recipeIndex = 0;

    private void Awake()
    {
        Recipe rec1 = new Recipe().Test();
        Recipe rec2 = new Recipe().Test2();
        Recipe rec3 = new Recipe().Test3();

        recipes = new Recipe[] { rec1, rec2, rec3 };
    }

    public Recipe GetNext()
    {
        Recipe next = recipes[recipeIndex];
        recipeIndex++;
        if (recipeIndex >= recipes.Length) recipeIndex = 0;
        print(recipeIndex);
        return next;
    }
}
