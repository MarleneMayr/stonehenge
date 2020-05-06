using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldChecker : MonoBehaviour
{
    private PhysicsBrick[] bricks;
    Recipe rec;

    private void Start()
    {
        bricks = FindObjectsOfType<PhysicsBrick>();
        rec = new Recipe().Test();
    }

    public void CheckRecipe()
    {
        foreach (var brick in bricks)
        {
            Debug.Log(brick.getOccupiedVoxels());
        }
        print("Match recipe: " + rec.MatchRecipe(bricks));
    }
}
