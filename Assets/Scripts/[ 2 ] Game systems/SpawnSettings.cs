using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnSettings", menuName = "SpawnSettings")]
public class SpawnSettings : ScriptableObject
{
    public SpawnChance[] chanceSettings;
    public float spawnIntervalMin;
    public float spawnIntervalMax;
    public int spawnAmountMin;
    public int spawnAmountMax;
}

[System.Serializable]
public class SpawnChance
{
    public GameObject gameItemPrefab;
    public float minChanceValue;
    public float maxChanceValue;
}