using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(this);
            return;
        }
    }
    #endregion
    public DataBall dataBall;
    public DataCup dataCup;
    public static float Fpush = 3.5f;
    public static int starMax = 27, starClaim = 0;
    public static int levelMax = 9, levelComplete = 0, levelCurrent = 1;
    public static string starsPerLevel = "";
    public static int money = 0;
    public static int cupId = 0, ballId = 0;
    public static int isPlaySound = 1, isPlayMusic = 1;
    public static ShopMode shopMode;

    public static void LoadGame()
    {
        starMax = 27; levelMax = 9;
        levelComplete = 0; starClaim = 0; starsPerLevel = "";
        cupId = PlayerPrefs.GetInt("cupId");
        ballId = PlayerPrefs.GetInt("ballId");
        isPlaySound = PlayerPrefs.GetInt("isPlaySound");
        isPlayMusic = PlayerPrefs.GetInt("isPlayMusic");
        levelComplete = PlayerPrefs.GetInt("levelComplete");
        starClaim = PlayerPrefs.GetInt("starClaim");
        starsPerLevel = PlayerPrefs.GetString("starsPerLevel");
        levelCurrent = levelComplete + 1;
        for (int i = starsPerLevel.Length; i < levelMax; i++)
        {
            starsPerLevel = starsPerLevel + "0";
        }
    }
    public static void SaveGame()
    {
        PlayerPrefs.SetInt("starClaim", starClaim);
        PlayerPrefs.SetInt("levelComplete", levelComplete);
        PlayerPrefs.SetInt("cupId", cupId);
        PlayerPrefs.SetInt("ballId", ballId);
        PlayerPrefs.SetString("starsPerLevel", starsPerLevel);
        PlayerPrefs.SetInt("isPlaySound", isPlaySound);
        PlayerPrefs.SetInt("isPlayMusic", isPlayMusic);
    }
}
