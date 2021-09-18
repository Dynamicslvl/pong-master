using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GamePauseScreen : MonoBehaviour
{
    public List<RectTransform> rects = new List<RectTransform>();
    public void Awake()
    {
        GameMaster.PauseLevel += Display;
        gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        foreach(RectTransform rect in rects) rect.localScale = Vector2.zero;
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
        PoolingSystem.instance.RecoverBall();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
