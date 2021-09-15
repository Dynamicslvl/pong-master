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
        GameMaster.Lose += ImagesOnLevelEnd;
    }
    public void OnDisable()
    {
        GameMaster.LoadLevel -= ImagesSetupOnLoadLevel;
        GameMaster.ShotBall -= BallRemainOnShotBall;
        GameMaster.Lose -= ImagesOnLevelEnd;
    }
    public void OnDestroy()
    {
        GameMaster.LoadLevel -= ImagesSetupOnLoadLevel;
        GameMaster.ShotBall -= BallRemainOnShotBall;
        GameMaster.Lose -= ImagesOnLevelEnd;
    }
    public void ImagesSetupOnLoadLevel()
    {
        SetImagesActive(true);
        taskImage.GetComponent<Image>().sprite = levelModeImages[(int)LevelController.levelContent.levelMode];
        for (int i = 0; i < 6; i++)
        {
            ballImages[i].GetComponent<Image>().sprite = ballSprites[0];
            if (i < LevelController.levelContent.numberOfBalls) ballImages[i].SetActive(true);
            else ballImages[i].SetActive(false);
        }
    }
    public void BallRemainOnShotBall()
    {
        ballImages[LevelController.ballLeft].GetComponent<Image>().sprite = ballSprites[1];
    }
    public void ImagesOnLevelEnd()
    {
        SetImagesActive(false);
    }
    public void SetImagesActive(bool value)
    {
        taskImage.SetActive(value);
        ballRemainer.SetActive(value);
    }
}
