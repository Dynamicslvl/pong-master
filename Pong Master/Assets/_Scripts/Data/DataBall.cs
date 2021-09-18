using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataBall", menuName = "ScriptableObject/DataBall", order = 2)]
public class DataBall : ScriptableObject
{
    [SerializeField] 
    public List<BallElements> ballTypes;
}

[System.Serializable]
public class BallElements
{
    public int id;
    public bool isSelect;
    public Sprite sprite;
}