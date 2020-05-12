using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private ColorPaletteUI colorReference;
    private ColorPaletteUI colors;

    private SpriteHighlight[] sprites;
    private bool isWarning = false;

    private void Awake()
    {
        sprites = FindObjectsOfType<SpriteHighlight>();
        colors = Instantiate(colorReference);
        colors.positive = GetColorWithAlpha(colorReference.positive, 0.5f);
        colors.negative = GetColorWithAlpha(colorReference.negative, 0.5f);
    }

    private Color GetColorWithAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public void HighlightSuccess()
    {
        foreach (var sprite in sprites)
        {
            sprite.HighlightOnce(colors.positive);
        }
        isWarning = false; // highlight once finishes on the initial color on purpose
    }

    public void ToggleTimeWarning(bool showWarning)
    {
        if (showWarning != isWarning)
        {
            isWarning = showWarning;
            if (showWarning)
            {
                foreach (var sprite in sprites)
                {
                    sprite.SetColor(colors.negative);
                }
            }
            else
            {
                foreach (var sprite in sprites)
                {
                    sprite.ResetColor();
                }
            }
        }
    }
}
