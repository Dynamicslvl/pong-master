using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePauseScreen : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public List<RectTransform> rects = new List<RectTransform>();
    public void Awake()
    {
        GameMaster.PauseLevel += Display;
        gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        levelText.text = "LEVEL " + LevelController.level.ToString("00");
        foreach (RectTransform rect in rects) rect.localScale = Vector2.zero;
        Sequence sq = DOTween.Sequence();
        foreach (RectTransform rect in rects)
        {
            sq.Append(rect.DOScale(Vector2.one, 0.08f));
        }
        sq.SetUpdate(true);
    }
    public void OnDestroy()
    {
        GameMaster.PauseLevel -= Display;
    }
    public void Display()
    {
        gameObject.SetActive(true);
    }
    public void ResumeLevel()
    {
        Sequence sq = DOTween.Sequence();
        foreach (RectTransform rect in rects)
        {
            sq.Append(rect.DOScale(Vector2.zero, 0.08f));
        }
        sq.SetUpdate(true).OnComplete(() =>
        {
            GameMaster.ResumeLevel?.Invoke();
            gameObject.SetActive(false);
        });
    }
    public void LoadMenu()
    {
        MenuController.instance.gameObject.SetActive(true);
        PoolingSystem.instance.RecoverBall();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
