using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteHighlight : MonoBehaviour
{
    private Color initialColor;
    private SpriteRenderer sprite;
    private Tween tw;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        initialColor = sprite.color;
    }

    public void HighlightOnce(Color color)
    {
        SetColor(color, 0.5f);
        tw.OnComplete(() => ResetColor(0.5f)); ;
    }

    public void SetColor(Color color, float fadeDuration = 0.2f)
    {
        tw.Kill();
        tw = sprite.DOColor(color, fadeDuration).SetEase(Ease.InOutSine);
    }

    public void ResetColor(float fadeDuration = 0.2f)
    {
        tw.Kill();
        tw = sprite.DOColor(initialColor, fadeDuration).SetEase(Ease.InOutSine);
    }
}
