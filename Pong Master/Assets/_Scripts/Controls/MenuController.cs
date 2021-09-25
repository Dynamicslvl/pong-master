using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class MenuController : MonoBehaviour
{
    public List<RectTransform> rect;
    RectTransform starClaim, levelComplete;
    public static MenuController instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }
        GameManager.LoadGame();
        AudioManager.instance.Play("BGM");
        rect[9].DOAnchorPosX(-125, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    private void OnEnable()
    {
        starNum = levelNum = 0;
        this.Wait(0.01f, () =>
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
        });
        rect[0].anchoredPosition = new Vector2(-100, -120);
        rect[1].anchoredPosition = new Vector2(160, -120);
        rect[2].anchoredPosition = new Vector2(0, 200);
        rect[6].anchoredPosition = new Vector2(-700, 360);
        rect[7].anchoredPosition = new Vector2(0, -100);
        rect[8].anchoredPosition = new Vector2(700, 360);
        rect[3].localScale = Vector2.zero;
        rect[4].localScale = Vector2.zero;
        rect[5].localScale = Vector2.zero;
        starClaim = rect[3].transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
        levelComplete = rect[4].transform.GetChild(1).GetChild(0).GetComponent<RectTransform>();
        starClaim.anchoredPosition = new Vector2(-200, 0);
        levelComplete.anchoredPosition = new Vector2(-200, 0);
        //DOTween
        rect[0].DOAnchorPos(new Vector2(120, -120), 0.5f).SetEase(Ease.OutExpo);
        rect[1].DOAnchorPos(new Vector2(-180, -120), 0.5f).SetEase(Ease.OutExpo);
        rect[2].DOAnchorPos(new Vector2(0, -585), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            
            rect[3].DOScale(Vector2.one, 0.3f);
            rect[4].DOScale(Vector2.one, 0.3f).OnComplete(() => {
                rect[5].DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    rect[5].DOScale(new Vector2(0.9f, 0.9f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                });
                rect[8].DOAnchorPos(new Vector2(220, 360), 0.5f).SetEase(Ease.OutExpo);
                rect[6].DOAnchorPos(new Vector2(-220, 360), 0.5f).SetEase(Ease.OutExpo);
                rect[7].DOAnchorPos(new Vector2(0, 360), 0.5f).SetEase(Ease.OutExpo);
                starClaim.DOAnchorPosX(-200 + 200f * GameManager.starClaim / GameManager.starMax, 0.5f).SetEase(Ease.InQuad); ;
                levelComplete.DOAnchorPosX(-200 + 200f * GameManager.levelComplete / GameManager.levelMax, 0.5f).SetEase(Ease.InQuad); ;
            });
        });
        
    }
    private int starNum, levelNum;
    private void Update()
    {
        if(starNum < GameManager.starClaim || levelNum < GameManager.levelComplete || GameManager.starClaim == 0)
        {
            starNum = (int)Mathf.Round((starClaim.anchoredPosition.x + 200) / 200f * GameManager.starMax);
            levelNum = (int)Mathf.Round((levelComplete.anchoredPosition.x + 200) / 200f * GameManager.levelMax);
            starClaim.parent.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "STAR: " + starNum.ToString() + "/" + GameManager.starMax.ToString();
            levelComplete.parent.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LEVEL: " + levelNum.ToString() + "/" + GameManager.levelMax.ToString();
        }
    }
    public void PlayButton()
    {
        AudioManager.instance.Play("TapButton");
        SceneManager.LoadScene("Gameplay");
        if (gameObject.activeSelf)
        {
            this.Wait(0.01f, () =>
            {
                DOTween.Kill(rect[5]);
                gameObject.SetActive(false);
            });
        }
    }
    public void OpenShop()
    {
        AudioManager.instance.Play("TapButton");
        transform.GetChild(10).gameObject.SetActive(true);
    }
    public void OpenChapter()
    {
        AudioManager.instance.Play("TapButton");
        ChapterManager.instance.gameObject.SetActive(true);
    }

    public void OpenSetting()
    {
        AudioManager.instance.Play("TapButton");
        transform.GetChild(11).gameObject.SetActive(true);
    }
}
