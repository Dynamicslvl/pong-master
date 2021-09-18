using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tag : MonoBehaviour
{
    public ShopMode shopMode;

    private void Awake()
    {
        GameMaster.TagClick += UpdateState;
    }
    private void OnDestroy()
    {
        GameMaster.TagClick -= UpdateState;
    }
    public void OnClick()
    {
        GameManager.shopMode = shopMode;
        GameMaster.TagClick?.Invoke();
    }
    private void UpdateState()
    {
        if(shopMode == GameManager.shopMode)
        {
            GetComponent<Image>().color = Color.yellow;
        } else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}
