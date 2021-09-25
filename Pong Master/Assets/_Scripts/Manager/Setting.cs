using UnityEngine.UI;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public Image[] buttonsIcon = new Image[2];
    public Sprite[] sprButtons = new Sprite[4];
    public virtual void Awake()
    {
        GameMaster.MusicClick += UpdateState;
        GameMaster.SoundClick += UpdateState;
        gameObject.SetActive(false);
    }
    public virtual void OnEnable()
    {
        UpdateState();
    }
    public virtual void OnDestroy()
    {
        GameMaster.MusicClick -= UpdateState;
        GameMaster.SoundClick -= UpdateState;
    }
    public void HomeButton()
    {
        AudioManager.instance.Play("TapButton");
        gameObject.SetActive(false);
    }

    public void SoundButton()
    {
        GameManager.isPlaySound = 1 - GameManager.isPlaySound;
        GameManager.SaveGame();
        AudioManager.instance.Play("TapButton");
        GameMaster.SoundClick?.Invoke();
    }

    public void MusicButton()
    {
        AudioManager.instance.Play("TapButton");
        GameManager.isPlayMusic = 1 - GameManager.isPlayMusic;
        GameManager.SaveGame();
        if (GameManager.isPlayMusic == 1) AudioManager.instance.Stop("BGM");
        else AudioManager.instance.Play("BGM");
        GameMaster.MusicClick?.Invoke();
    }

    public virtual void UpdateState()
    {
        buttonsIcon[0].sprite = GameManager.isPlaySound == 0 ? sprButtons[0] : sprButtons[1];
        buttonsIcon[1].sprite = GameManager.isPlayMusic == 0 ? sprButtons[2] : sprButtons[3];
    }
}
