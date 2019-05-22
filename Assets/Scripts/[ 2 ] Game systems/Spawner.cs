using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : GameSystem
{
    [SerializeField] SpawnSettings settings;
    [SerializeField] Transform leftTopBound;
    [SerializeField] Transform rightBottomBound;
        

    IEnumerator spawnCoroutine;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawn()
    {
        StopSpawn();

        spawnCoroutine = Spawn();
        StartCoroutine(spawnCoroutine);

    }

    public void StopSpawn()
    {
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    GameObject GetRandomGameItemPrefab()
    {
        float chanceValue = Random.Range(0, 99.9f);
        GameObject prefab = null;
        for(int i = 0; i < settings.chanceSettings.Length; i++)
        {
            if(chanceValue >= settings.chanceSettings[i].minChanceValue && chanceValue < settings.chanceSettings[i].maxChanceValue)
            {
                prefab = settings.chanceSettings[i].gameItemPrefab;
                break;
            }
        }

        return prefab;
    }

    IEnumerator Spawn()
    {
        float spawnInterval = Random.Range(settings.spawnIntervalMin, settings.spawnIntervalMax);
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            //Spawn
            int objectsAmount = Random.Range(settings.spawnAmountMin, settings.spawnAmountMax + 1);
            for(int i = 0; i < objectsAmount; i++)
            {
                GameObject prefab = GetRandomGameItemPrefab();
                if (prefab != null)
                {
                    float randomPosX = Random.Range(leftTopBound.position.x, rightBottomBound.position.x);
                    float randomPosY = Random.Range(leftTopBound.position.y, rightBottomBound.position.y);
                    GameObject newGameItem = Instantiate(prefab,new Vector3(randomPosX, randomPosY, 0),Quaternion.Euler(Vector3.zero));
                }
            }            

            spawnInterval = Random.Range(settings.spawnIntervalMin, settings.spawnIntervalMax);
        }

    }

}
