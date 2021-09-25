using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cup : MonoBehaviour
{
    bool isDestroy = false;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.dataCup.cupTypes[GameManager.cupId].sprite;
    }
    private void Update()
    {
        if (transform.position.y < -15f && LevelController.levelState == LevelState.Playing && !isDestroy) GameLost();
        float angle = transform.rotation.eulerAngles.z + 90;
        if (angle > 360) angle -= 360;
        if ((angle <= 20 || angle >= 160) && !isDestroy) GameLost();
    }
    private void GameLost()
    {
        //Debug.Log("Level failed because cup fall!");
        LevelController.levelState = LevelState.Lose;
        GameMaster.Lose?.Invoke();
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            isDestroy = true;
            LevelController.taskComplete++;
            Destroy(GetComponent<BoxCollider2D>());
            PoolingSystem.instance.GivePraise(transform.position);
            this.Wait(0.3f, () =>
            {
                transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuart);
                GetComponent<SpriteRenderer>().DOFade(0, 0.5f).SetEase(Ease.InQuart);
            });
        }
    }
}
