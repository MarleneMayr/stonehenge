using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Colors/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    [SerializeField] private Color[] colors;
    public Color[] Colors => colors;

    private void OnValidate()
    {
        foreach (var color in colors)
        {
            if (color.a < 0.5f)
            {
                Debug.LogWarning($"Color alpha value {color.a} will not be visible on the brick!");
            }
        }
    }
}
