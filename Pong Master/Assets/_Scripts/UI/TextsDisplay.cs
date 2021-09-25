using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextsDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelTask;
    public TextMeshProUGUI levelMode;
    public TextMeshProUGUI levelNumber;
    public TextMeshProUGUI levelDifficulty;
    public void OnEnable()
    {
        GameMaster.LoadLevel += TextsSetup;
        GameMaster.NewTaskComplete += TextsSetup;
        GameMaster.Lose += TextsOnLevelEnd;
        GameMaster.Win += TextsOnLevelEnd;
    }
    public void OnDisable()
    {
        GameMaster.LoadLevel -= TextsSetup;
        GameMaster.NewTaskComplete -= TextsSetup;
        GameMaster.Lose -= TextsOnLevelEnd;
        GameMaster.Win -= TextsOnLevelEnd;
    }
    public void OnDestroy()
    {
        GameMaster.LoadLevel -= TextsSetup;
        GameMaster.NewTaskComplete -= TextsSetup;
        GameMaster.Lose -= TextsOnLevelEnd;
        GameMaster.Win -= TextsOnLevelEnd;
    }
    public void TextsSetup()
    {
        SetTextsActive(true);
        levelTask.text = LevelController.taskComplete.ToString() + "/" + LevelController.taskNumber.ToString();
        levelMode.text = System.Enum.GetName(typeof(LevelMode), LevelController.levelContent.levelMode).Replace("_", " ");
        levelNumber.text = "LEVEL " + GameManager.levelCurrent.ToString();
        levelDifficulty.text = System.Enum.GetName(typeof(LevelDifficulty), LevelController.levelContent.levelDifficulty);
    }

    public void TextsOnLevelEnd()
    {
        SetTextsActive(false);
    }

    public void SetTextsActive(bool value)
    {
        levelTask.gameObject.SetActive(value);
        levelMode.gameObject.SetActive(value);
        levelNumber.gameObject.SetActive(value);
        levelDifficulty.gameObject.SetActive(value);
    }
}
