using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public GameObject[] buttons = new GameObject[3];
    
    public void OnEnable()
    {
        GameMaster.LoadLevel += ButtonsOnLoadLevel;
        GameMaster.Lose += ButtonsOnLevelEnd;
        GameMaster.Win += ButtonsOnLevelEnd;
    }
    public void OnDisable()
    {
        GameMaster.LoadLevel -= ButtonsOnLoadLevel;
        GameMaster.Lose -= ButtonsOnLevelEnd;
        GameMaster.Win -= ButtonsOnLevelEnd;
    }
    public void OnDestroy()
    {
        GameMaster.LoadLevel -= ButtonsOnLoadLevel;
        GameMaster.Lose -= ButtonsOnLevelEnd;
        GameMaster.Win -= ButtonsOnLevelEnd;
    }
    public void ButtonsOnLoadLevel()
    {
        SetButtonsActive(true);
    }
    public void ButtonsOnLevelEnd()
    {
        SetButtonsActive(false);
    }
    public void SetButtonsActive(bool value)
    {
        foreach(GameObject button in buttons)
        {
            button.SetActive(value);
        }
    }
    public void RestartLevel()
    {
        GameMaster.RestartLevel?.Invoke();
    }
}
