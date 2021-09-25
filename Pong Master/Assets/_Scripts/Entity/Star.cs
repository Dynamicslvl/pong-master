using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ParticleSystem effect = PoolingSystem.instance.GiveEffectStar(transform.position);
            effect.gameObject.SetActive(true);
            effect.Play();
            LevelController.taskComplete++;
            Destroy(gameObject);
        }
    }
}
