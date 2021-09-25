using UnityEngine.UI;
using UnityEngine;

public class Level : MonoBehaviour
{
    [HideInInspector] public int levelId;
    public GameObject[] stars = new GameObject[3];
    public Sprite[] imageButton = new Sprite[3];
    public void UpdateProgress()
    {
        int starClaimInLevel = (int) (GameManager.starsPerLevel[levelId - 1] - '0');
        for(int i = 0; i<3; i++)
        {
            if(i < starClaimInLevel)
            {
                stars[i].SetActive(true);
            } else
            {
                stars[i].SetActive(false);
            }
        }
        GetComponent<Image>().sprite = imageButton[0];
        GetComponent<Button>().interactable = true;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(false);
        if (levelId == GameManager.levelComplete + 1)
        {
            GetComponent<Image>().sprite = imageButton[1];
        }
        if(levelId > GameManager.levelComplete + 1)
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().sprite = imageButton[2];
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }
    }
    public void OnClick()
    {
        AudioManager.instance.Play("TapButton");
        GameManager.levelCurrent = levelId;
        MenuController.instance.PlayButton();
        this.Wait(0.01f, () =>
        {
            Time.timeScale = 1;
            ChapterManager.instance.BackToChapters(false);
            ChapterManager.instance.BackToHome(false);
        });
    }
}
