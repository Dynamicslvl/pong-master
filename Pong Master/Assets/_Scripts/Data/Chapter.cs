using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Chapter : MonoBehaviour
{
    [HideInInspector] public int levelStart, length, chapterId;
    [HideInInspector] public GameObject levelPanel, chapterPanel;
    [HideInInspector] public TextMeshProUGUI banner;
    public Sprite[] imageBorder = new Sprite[3];
    public TextMeshProUGUI starClaim;
    private Transform level;
    public void UpdateProgress()
    {
        starClaim.text = TotalStarClaimInChapter().ToString("00") + "/" + (length * 3).ToString("00");
        transform.GetChild(1).GetComponent<Image>().sprite = imageBorder[0];
        transform.GetChild(1).GetComponent<Button>().interactable = true;
        if (levelStart > GameManager.levelComplete + 1)
        {
            transform.GetChild(1).GetComponent<Button>().interactable = false;
            transform.GetChild(1).GetComponent<Image>().sprite = imageBorder[2];
        } else
        {
            if(levelStart + length > GameManager.levelComplete + 1)
            {
                transform.GetChild(1).GetComponent<Image>().sprite = imageBorder[1];
            }
        }
    }
    public void OnClick()
    {
        AudioManager.instance.Play("TapButton");
        banner.text = "CHAPTER " + (chapterId + 1).ToString("00");
        ChapterManager.instance.chapterCurrent = chapterId;
        chapterPanel.SetActive(false);
        levelPanel.SetActive(true);

        for(int i = 0; i<12; i++)
        {
            level = levelPanel.transform.GetChild(0).GetChild(i);
            if (i < length)
            {
                level.GetComponent<Level>().levelId = levelStart + i;
                level.GetComponent<Level>().UpdateProgress();
                level.gameObject.SetActive(true);
                level.GetChild(0).GetComponent<TextMeshProUGUI>().text = (levelStart + i).ToString("00");
            } else
            {
                level.gameObject.SetActive(false);
            }
        }
    }
    private int TotalStarClaimInChapter()
    {
        int sum = 0;
        string s = GameManager.starsPerLevel;
        for(int i = levelStart; i < levelStart + length; i++)
        {
            sum += (int) (s[i-1] - '0');
        }
        return sum;
    }
}
