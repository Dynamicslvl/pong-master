using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public BallElements ballElements;
    public CupElements cupElements;

    private void OnEnable()
    {
        GameMaster.SlotClick += UpdateState;
        UpdateState();
    }
    private void OnDisable()
    {
        GameMaster.SlotClick -= UpdateState;
    }
    public void OnClick()
    {
        AudioManager.instance.Play("TapButton");
        if (ballElements != null)
        {
            GameManager.ballId = ballElements.id;
            ballElements.isSelect = true;
        }
        if (cupElements != null)
        {
            GameManager.cupId = cupElements.id;
            cupElements.isSelect = true;
        }
        GameMaster.SlotClick?.Invoke();
        GameManager.SaveGame();
    }
    private void UpdateState()
    {
        if (ballElements != null)
        {
            if (GameManager.ballId != ballElements.id)
            {
                ballElements.isSelect = false;
                UnSelectedState();
            } else
            {
                ballElements.isSelect = true;
                SelectedState();
            }
        }
        if (cupElements != null)
        {
            if (GameManager.cupId != cupElements.id)
            {
                cupElements.isSelect = false;
                UnSelectedState();
            } else
            {
                cupElements.isSelect = true;
                SelectedState();
            }
        }
    }

    private void SelectedState()
    {
        transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GetComponent<Image>().color = new Color(0, 1, 1, 1);
    }

    private void UnSelectedState()
    {
        transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
}
