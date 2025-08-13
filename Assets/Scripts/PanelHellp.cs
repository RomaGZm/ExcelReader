using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelHellp : MonoBehaviour
{
    [Tooltip("Сколько секунд длится анимация")]
    public float duration = 0.3f;

    [Tooltip("Показывать при старте автоматически")]
    public bool playOnStart = true;
    public Transform frame;

    private void Start()
    {
        if (playOnStart)
            Show();
    }

    public void Show()
    {
        frame.transform.localScale = Vector3.zero; // Сначала спрятать
        gameObject.SetActive(true);
        frame.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        frame.transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));
    }
    public void OnBtnHelpCloseClick()
    {
        Hide();
    }
}
