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
    }
    public void OnDisable()
    {
        GameMaster.LoadLevel -= TextsSetup;
        GameMaster.NewTaskComplete -= TextsSetup;
    }
    public void OnDestroy()
    {
        GameMaster.LoadLevel -= TextsSetup;
        GameMaster.NewTaskComplete -= TextsSetup;
    }
    public void TextsSetup()
    {
        levelTask.text = LevelController.taskComplete.ToString() + "/" + LevelController.taskNumber.ToString();
        levelMode.text = System.Enum.GetName(typeof(LevelMode), LevelController.levelContent.levelMode).Replace("_", " ");
        levelNumber.text = "LEVEL " + LevelController.level.ToString();
        levelDifficulty.text = System.Enum.GetName(typeof(LevelDifficulty), LevelController.levelContent.levelDifficulty);
    }
}
