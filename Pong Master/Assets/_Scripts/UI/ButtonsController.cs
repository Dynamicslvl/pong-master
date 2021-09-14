using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public void RestartLevel()
    {
        PoolingSystem.instance.RecoverBall();
        GameMaster.RestartLevel?.Invoke();
    }
}
