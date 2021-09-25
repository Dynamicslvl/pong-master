using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject cupContent, ballContent;
    public DataCup dataCup;
    public DataBall dataBall;

    private void Awake()
    {
        GameManager.LoadGame();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.shopMode = ShopMode.Cups;
        for (int i = 0; i < 9; i++)
        {
            cupContent.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = dataCup.cupTypes[i].sprite;
            cupContent.transform.GetChild(i).GetChild(0).GetComponent<Image>().SetNativeSize();
            cupContent.transform.GetChild(i).GetComponent<Slot>().cupElements = dataCup.cupTypes[i];
            cupContent.transform.GetChild(i).GetComponent<Slot>().ballElements = null;
        }
        for (int i = 0; i < 18; i++)
        {
            ballContent.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = dataBall.ballTypes[i].sprite;
            ballContent.transform.GetChild(i).GetChild(0).GetComponent<Image>().SetNativeSize();
            ballContent.transform.GetChild(i).GetComponent<Slot>().ballElements = dataBall.ballTypes[i];
            ballContent.transform.GetChild(i).GetComponent<Slot>().cupElements = null;
        }
        GameMaster.SlotClick?.Invoke();
        GameMaster.TagClick?.Invoke();
    }

    private void Update()
    {
        if(GameManager.shopMode == ShopMode.Balls)
        {
            transform.GetChild(6).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
        } else
        {
            transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
            transform.GetChild(6).GetChild(1).gameObject.SetActive(false);
        }
    }

    public void CloseShop()
    {
        AudioManager.instance.Play("TapButton");
        gameObject.SetActive(false);
    }
}
