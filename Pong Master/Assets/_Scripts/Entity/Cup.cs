using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cup : MonoBehaviour
{
    bool isDestroy = false;
    private void Update()
    {
        if (transform.position.y < -15f && LevelController.levelState == LevelState.Playing && !isDestroy)
        {
            Debug.Log("Level failed because cup fall!");
            LevelController.levelState = LevelState.Lose;
            GameMaster.Lose?.Invoke();
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        DOTween.Pause(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            isDestroy = true;
            Destroy(GetComponent<BoxCollider2D>());
            this.Wait(0.5f, () =>
            {
                transform.DOScale(Vector3.zero, 0.5f);
                GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            });
        }
    }
}
