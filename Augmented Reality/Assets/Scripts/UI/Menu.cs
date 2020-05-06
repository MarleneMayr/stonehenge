using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    private CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    public void Show(float fadeDuration = 0.5f)
    {
        canvas.DOFade(endValue: 1.0f, duration: fadeDuration).SetEase(Ease.OutSine);
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    public void Hide(float fadeDuration = 0.5f)
    {
        canvas.DOFade(endValue: 0.0f, duration: fadeDuration).SetEase(Ease.InSine);
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
