using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePauseScreen : Setting
{
    public TextMeshProUGUI levelText;
    public List<RectTransform> rects = new List<RectTransform>();
    public override void Awake()
    {
        GameMaster.MusicClick += UpdateState;
        GameMaster.SoundClick += UpdateState;
        GameMaster.PauseLevel += Display;
        gameObject.SetActive(false);
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        levelText.text = "LEVEL " + GameManager.levelCurrent.ToString("00");
        foreach (RectTransform rect in rects) rect.localScale = Vector2.zero;
        Sequence sq = DOTween.Sequence();
        foreach (RectTransform rect in rects)
        {
            sq.Append(rect.DOScale(Vector2.one, 0.08f));
        }
        sq.SetUpdate(true);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        GameMaster.PauseLevel -= Display;
    }
    public void Display()
    {
        gameObject.SetActive(true);
    }
    public void ResumeLevel()
    {
        AudioManager.instance.Play("TapButton");
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
        AudioManager.instance.Play("TapButton");
        MenuController.instance.gameObject.SetActive(true);
        PoolingSystem.instance.RecoverPool();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void LoadChapter()
    {
        AudioManager.instance.Play("TapButton");
        ChapterManager.instance.gameObject.SetActive(true);
    }
}
