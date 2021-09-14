using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cup : MonoBehaviour
{
    private void OnDestroy()
    {
        DOTween.PauseAll();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Destroy(GetComponent<BoxCollider2D>());
            this.Wait(0.5f, () =>
            {
                transform.DOScale(Vector3.zero, 1f);
                GetComponent<SpriteRenderer>().DOFade(0, 1f);
            });
        }
    }
}
