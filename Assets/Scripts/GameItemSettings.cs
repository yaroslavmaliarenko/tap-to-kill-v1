using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameItemSettings", menuName = "GameItemSettings")]
public class GameItemSettings : ScriptableObject
{
    [SerializeField] PointsOperationType pointType;
    [SerializeField] int pointCount;
    [SerializeField] bool useLifeTime;
    [SerializeField] float lifeTime;

    public PointsOperationType PointType { get => pointType;}
    public int PointCount { get => pointCount;}
    public bool UseLifeTime { get => useLifeTime;}
    public float LifeTime { get => lifeTime; }
}
