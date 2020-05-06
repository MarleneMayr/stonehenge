using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    private Brick[] ingredients;

    public Recipe Test()
    {
        Voxel[] temp = new Voxel[3] { new Voxel(new Vector3Int(-1, 0, -1)), new Voxel(new Vector3Int(0, 0, -1)), new Voxel(new Vector3Int(-2, 0, -1)) };
        Brick b = new Brick(Brick.ID.red, temp);
        ingredients = new Brick[1] { b };

        return this;
    }
    public Recipe Test2()
    {
        Voxel[] temp = new Voxel[3] { new Voxel(new Vector3Int(-1, 0, 1)), new Voxel(new Vector3Int(0, 0, 1)), new Voxel(new Vector3Int(-2, 0, 1)) };
        Brick b = new Brick(Brick.ID.red, temp);
        ingredients = new Brick[1] { b };

        return this;
    }
    public Recipe Test3()
    {
        Voxel[] temp = new Voxel[3] { new Voxel(new Vector3Int(-1, 0, -1)), new Voxel(new Vector3Int(0, 0, -1)), new Voxel(new Vector3Int(1, 0, -1)) };
        Brick b = new Brick(Brick.ID.red, temp);
        ingredients = new Brick[1] { b };

        return this;
    }

    public bool MatchRecipe(PhysicsBrick[] bricks)
    {
        foreach (var i in ingredients)
        {
            // if any ingredient is missing, the recipe does not match the bricks
            if (MatchIngredient(i, bricks) == false) return false;
        }
        return true;
    }

    private bool MatchIngredient(Brick ingredient, PhysicsBrick[] bricks)
    {
        // check if any of the brick matches the one in the recipe
        foreach (var brick in bricks)
        {
            if (brick.match(ingredient) == true) return true;
        }
        // return false if none of the placed bricks matches
        return false;
    }
}
