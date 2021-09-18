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
    public static int starMax = 36, starClaim = 12;
    public static int levelMax = 12, levelComplete = 6;
    public static int money;
}
