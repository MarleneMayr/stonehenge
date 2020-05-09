using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bricks;

public class MoldChecker : MonoBehaviour
{
    public UnityEvent OnMoldMatch;

    private PhysicsBrick[] bricks;
    private Recipe currentRecipe;

    private void Start()
    {
        bricks = FindObjectsOfType<PhysicsBrick>();
        print(bricks.Length + " physicsbricks found in the game.");
    }

    public void StartChecking(Recipe recipe, float interval = 1)
    {
        currentRecipe = recipe;
        StartCoroutine(CheckContinuously(interval));
    }

    public void StopChecking()
    {
        StopAllCoroutines();
    }

    private IEnumerator CheckContinuously(float interval)
    {
        if (currentRecipe == null) yield break;

        yield return new WaitForSeconds(interval);

        // as soon as the recipe matches, the coroutine will end and invoke the according event
        while (!CheckRecipe())
        {
            yield return new WaitForSeconds(interval);
        }
    }

    private bool CheckRecipe()
    {
        // important, update the voxel values of all bricks here
        // TODO extract this into some sort of brick manager that handles all current physicsbricks in the game
        UpdateAllActivePhysicsBricks();

        bool isMatching = MatchRecipe(currentRecipe);
        if (isMatching) OnMoldMatch.Invoke();
        return isMatching;
    }

    private void UpdateAllActivePhysicsBricks()
    {
        foreach (var brick in bricks)
        {
            brick.UpdateVoxels();
        }
    }

    public bool MatchRecipe(Recipe rec)
    {
        foreach (var i in rec.ingredients)
        {
            // if any ingredient is missing, the recipe does not match the bricks
            if (MatchIngredient(i) == false) return false;
        }
        return true;
    }

    private bool MatchIngredient(RecipeBrick ingredient)
    {
        // check if any of the brick matches the one in the recipe
        foreach (var brick in bricks)
        {
            if (brick.Match(ingredient) == true) return true;
        }
        // return false if none of the placed bricks matches
        return false;
    }
}
