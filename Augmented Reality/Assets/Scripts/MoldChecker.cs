using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MoldChecker : MonoBehaviour
{
    public UnityEvent OnMoldMatch;

    private PhysicsBrick[] bricks;
    private Recipe rec;

    private void Start()
    {
        bricks = FindObjectsOfType<PhysicsBrick>();
    }

    public void StartChecking(Recipe recipe, float interval = 1)
    {
        rec = recipe;
        StartCoroutine(CheckContinuously(interval));
    }

    public void StopChecking()
    {
        StopAllCoroutines();
    }

    private IEnumerator CheckContinuously(float interval)
    {
        bool match = false;
        while (!match)
        {
            yield return new WaitForSeconds(interval);
            match = CheckRecipe();
        }
    }

    private bool CheckRecipe()
    {
        foreach (var brick in bricks)
        {
            brick.updateVoxels(); // important, update the voxel values of all bricks here
            print(brick.ToString());
        }
        bool isMatching = rec.MatchRecipe(bricks);
        print("Match recipe: " + isMatching);

        if (isMatching) OnMoldMatch.Invoke();
        return isMatching;
    }
}
