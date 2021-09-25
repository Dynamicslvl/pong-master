using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameWonScreen : MonoBehaviour
{
    private TextMeshProUGUI levelText;
    public List<RectTransform> rects = new List<RectTransform>();
    public List<RectTransform> stars = new List<RectTransform>();
    public void Awake()
    {
        levelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        GameMaster.Win += Display;
        GameMaster.LoadLevel += Hide;
        gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        AudioManager.instance.Play("Firework");
        levelText.text = "LEVEL " + GameManager.levelCurrent.ToString("00");
    }
    public void OnDestroy()
    {
        GameMaster.Win -= Display;
        GameMaster.LoadLevel -= Hide;
    }
    public void Display()
    {
        gameObject.SetActive(true);

        rects[0].anchoredPosition = new Vector2(0, 350);
        rects[1].anchoredPosition = new Vector2(0, 200);
        rects[2].localScale = rects[3].localScale = Vector3.zero;
        rects[4].anchoredPosition = new Vector2(-700, -428);
        rects[5].anchoredPosition = new Vector2(800, -428);
        rects[6].anchoredPosition = new Vector2(0, -100);

        rects[0].DOAnchorPos(new Vector2(0, -316), 0.5f);
        rects[1].DOAnchorPos(new Vector2(0, -485), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            rects[4].DOAnchorPos(new Vector2(-258, -428), 0.5f).SetEase(Ease.OutExpo);
            rects[5].DOAnchorPos(new Vector2(91, -428), 0.5f).SetEase(Ease.OutExpo);
            rects[6].DOAnchorPos(new Vector2(0, 379), 0.5f).SetEase(Ease.OutExpo);
        });
        rects[2].DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            rects[3].DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        });
        Sequence sq = DOTween.Sequence();
        for (int i = 0; i < 3; i++) {
            stars[i].localScale = Vector3.zero;
        }
        for(int i = 0; i<=Mathf.Min(2, LevelController.ballLeft); i++)
        {
            sq.Append(stars[i].DOScale(Vector3.one, 0.3f)).Join(transform.DOScale(Vector3.one, 0).OnComplete(() =>{
                AudioManager.instance.Play("StarAppear");
            }));
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void NextLevel()
    {
        AudioManager.instance.Play("TapButton");
        GameMaster.SkipLevel?.Invoke();
    }
}
