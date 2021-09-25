using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Praise : MonoBehaviour
{
    public Sprite[] praises;
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        GetComponent<SpriteRenderer>().sprite = praises[Random.Range(0, praises.Length)];
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        transform.DOMove(transform.position + new Vector3(0, 2.5f, 0), 0.3f);
        transform.DOScale(Vector3.one, 0.3f);
        GetComponent<SpriteRenderer>().DOFade(1, 0.3f);
        this.Wait(0.5f, () =>
        {
            GetComponent<SpriteRenderer>().DOFade(0, 0.3f);
        });
    }
}
