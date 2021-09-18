using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataCup", menuName = "ScriptableObject/DataCup", order = 1)]
public class DataCup : ScriptableObject
{
    [SerializeField]
    public List<CupElements> cupTypes;
}

[System.Serializable]
public class CupElements
{
    public int id;
    public bool isSelect;
    public Sprite sprite;
}