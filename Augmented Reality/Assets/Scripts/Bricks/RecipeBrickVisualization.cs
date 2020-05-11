using UnityEngine;

public class RecipeBrickVisualization : MonoBehaviour
{
    private Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void SetColor(Color color)
    {
        mat.SetColor("_Color", color);
    }
}
