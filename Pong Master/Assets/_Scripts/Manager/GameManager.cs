using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    #region SINGLETON
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
    public static int starMax = 9, starClaim = 0;
    public static int levelMax = 3, levelComplete = 0;
    public static string starsPerLevel = "";
    public static int money = 0;
    public static int cupId = 0, ballId = 0;
    public static ShopMode shopMode;

    public static void LoadGame()
    {
        levelComplete = PlayerPrefs.GetInt("levelComplete");
        cupId = PlayerPrefs.GetInt("cupId");
        ballId = PlayerPrefs.GetInt("ballId");
        starClaim = PlayerPrefs.GetInt("starClaim");
        starsPerLevel = PlayerPrefs.GetString("starsPerLevel");
        for (int i = starsPerLevel.Length; i<=levelMax; i++)
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
    }
}
