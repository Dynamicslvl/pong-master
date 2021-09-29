using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterManager : MonoBehaviour
{
    #region SINGLETON
    public static ChapterManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public static int[] chapterLength = {8, 1};
    public GameObject content;
    public GameObject levelPanel, chapterPanel;
    public int chapterCurrent;
    public TextMeshProUGUI banner;
    private Chapter chapter;

    private void OnEnable()
    {
        int levelStart = 1;
        for(int i = 0; i<chapterLength.Length; i++)
        {
            chapter = content.transform.GetChild(i).GetComponent<Chapter>();
            chapter.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "CHAPTER " + (i + 1).ToString("00");
            chapter.levelStart = levelStart;
            chapter.length = chapterLength[i];
            chapter.chapterPanel = chapterPanel;
            chapter.levelPanel = levelPanel;
            chapter.banner = banner;
            chapter.chapterId = i;
            chapter.UpdateProgress();
            levelStart += chapterLength[i];
        }
    }

    public void BackToChapters(bool isClicked)
    {
        if(isClicked) AudioManager.instance.Play("TapButton");
        banner.text = "CHAPTERS";
        levelPanel.SetActive(false);
        chapterPanel.SetActive(true);
    }

    public void BackToHome(bool isClicked)
    {
        if(isClicked) AudioManager.instance.Play("TapButton");
        gameObject.SetActive(false);
    }

    public void NextChapter()
    {
        chapterCurrent++;
        if (chapterCurrent == chapterLength.Length) chapterCurrent = 0;
        content.transform.GetChild(chapterCurrent).GetComponent<Chapter>().OnClick();
    }

    public void PreviousChapter()
    {
        chapterCurrent--;
        if (chapterCurrent < 0) chapterCurrent = chapterLength.Length - 1;
        content.transform.GetChild(chapterCurrent).GetComponent<Chapter>().OnClick();
    }
}
