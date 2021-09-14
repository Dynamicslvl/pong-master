using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesDisplay : MonoBehaviour
{
    public GameObject taskImage;
    public GameObject ballRemainer;
    public Sprite[] ballSprites = new Sprite[2];
    public Sprite[] levelModeImages = new Sprite[3];
    private List<GameObject> ballImages = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            ballImages.Add(ballRemainer.transform.GetChild(i).gameObject);
        }
    }
    public void OnEnable()
    {
        GameMaster.LoadLevel += ImagesSetupOnLoadLevel;
        GameMaster.ShotBall += BallRemainOnShotBall;
    }
    public void OnDisable()
    {
        GameMaster.LoadLevel -= ImagesSetupOnLoadLevel;
        GameMaster.ShotBall -= BallRemainOnShotBall;
    }
    public void OnDestroy()
    {
        GameMaster.LoadLevel -= ImagesSetupOnLoadLevel;
        GameMaster.ShotBall -= BallRemainOnShotBall;
    }
    public void ImagesSetupOnLoadLevel()
    {
        taskImage.GetComponent<Image>().sprite = levelModeImages[(int)LevelController.levelContent.levelMode];
        for (int i = 0; i < 6; i++)
        {
            if (i < LevelController.levelContent.numberOfBalls) ballImages[i].SetActive(true);
            else ballImages[i].SetActive(false);
        }
    }
    public void BallRemainOnShotBall()
    {
        for(int i = LevelController.levelContent.numberOfBalls - 1; i>=0; i--)
        {
            if(ballImages[i].GetComponent<Image>().sprite == ballSprites[0])
            {
                ballImages[i].GetComponent<Image>().sprite = ballSprites[1];
                break;
            }
        }
    }
}
