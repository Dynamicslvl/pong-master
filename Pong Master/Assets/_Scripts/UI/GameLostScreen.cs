using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameLostScreen : MonoBehaviour
{
    private TextMeshProUGUI levelText;
    private RectTransform replayButton, skipButton;
    public void Awake()
    {
        levelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        replayButton = transform.GetChild(1).GetComponent<RectTransform>();
        skipButton = transform.GetChild(2).GetComponent<RectTransform>();
        GameMaster.LoadLevel += Hide;
        GameMaster.Lose += Display;
        gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        levelText.text = "LEVEL " + LevelController.level.ToString("00");
    }
    public void OnDestroy()
    {
        GameMaster.Lose -= Display;
        GameMaster.LoadLevel -= Hide;
    }
    public void Display()
    {
        gameObject.SetActive(true);
        Sequence sq = DOTween.Sequence();
        replayButton.localScale = Vector3.zero;
        skipButton.localScale = Vector3.zero;
        sq.Append(replayButton.DOScale(Vector3.one, 0.2f))
          .Append(skipButton.DOScale(Vector3.one, 0.2f));
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void RestartLevel()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(replayButton.DOScale(Vector3.zero, 0.2f))
          .Append(skipButton.DOScale(Vector3.zero, 0.2f)).AppendCallback(() =>
          {
              GameMaster.RestartLevel?.Invoke();
          });
    }
    public void SkipLevel()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(skipButton.DOScale(Vector3.zero, 0.2f))
          .Append(replayButton.DOScale(Vector3.zero, 0.2f)).AppendCallback(() =>
          {
              GameMaster.SkipLevel?.Invoke();
          });
    }
}
