using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class GameItem : MonoBehaviour,IPointerDownHandler
{
    [SerializeField]GameItemSettings itemSettings;
    PointsOperationType pointType;
    int pointCount;
    bool useLifeTime;
    float lifeTime;

    GameLogic gl;

    private void Awake()
    {
        pointType = itemSettings.PointType;
        pointCount = itemSettings.PointCount;
        useLifeTime = itemSettings.UseLifeTime;
        lifeTime = itemSettings.LifeTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (useLifeTime) Destroy(gameObject, lifeTime);
        gl = GameSystemsManager.Instance.GetSystem<GameLogic>();
        if (gl != null) gl.endGameEvent += OnEndGameEventProcessing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.timeScale == 0) return;

        ScoreManager scoreManager = GameSystemsManager.Instance.GetSystem<ScoreManager>();
        scoreManager?.UpdateScore(pointCount, pointType, transform.position);
        Destroy(gameObject);
    }

    void OnEndGameEventProcessing()
    {        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (gl != null) gl.endGameEvent -= OnEndGameEventProcessing;
    }
}
