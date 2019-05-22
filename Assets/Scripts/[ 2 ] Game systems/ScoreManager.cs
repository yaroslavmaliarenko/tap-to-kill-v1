using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : GameSystem
{
    int gameScore;
    int gameScoreBuff;
    [SerializeField] GameObject scoreTextGO;
    [SerializeField] GameObject scoreLabelGO;
    [SerializeField] Transform canvasT;
    [SerializeField] GameObject addScoreAnimationPrefab;
    [SerializeField] GameObject deductScoreAnimationPrefab;
    
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextGO.GetComponent<Text>();
        scoreText.text = gameScoreBuff.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScoreTextActive(bool isActive)
    {
        scoreTextGO.SetActive(isActive);
        scoreLabelGO.SetActive(isActive);
    }

    public void UpdateScore(int points,PointsOperationType opType,Vector3 position)
    {
        if(opType == PointsOperationType.INCREASE)
        {
            gameScore += points;
        }
        else
        {
            gameScore -= points;
        }

        StartCoroutine(AddPoints(points, opType));
        StartCoroutine(AddPointsAnimation(points, opType,position));
    }
   

    

    IEnumerator AddPoints(int points, PointsOperationType opType)
    {        
        while (points > 0)
        {
            yield return new WaitForSeconds(0.002f);
            if(opType == PointsOperationType.INCREASE) gameScoreBuff++;
            else gameScoreBuff--;
            scoreText.text = "" + gameScoreBuff;
            points--;
        }

    }

    IEnumerator AddPointsAnimation(int points, PointsOperationType opType, Vector3 position)
    {
        GameObject newTextObj = Instantiate<GameObject>(opType == PointsOperationType.INCREASE ? addScoreAnimationPrefab : deductScoreAnimationPrefab, position, new Quaternion(0, 0, 0, 0), canvasT);
        Text pointsText = newTextObj.GetComponent<Text>();
        pointsText.text = (opType == PointsOperationType.INCREASE ? "+" : "-") + points;
        RectTransform rectT = newTextObj.GetComponent<RectTransform>();
        Vector3 newPos = position + Vector3.up * 1.5f;

        float velocity = 0;
        float smoothTime = 0.15f;
        while (true)
        {
            var newY = Mathf.SmoothDamp(rectT.position.y, newPos.y, ref velocity, smoothTime);
            rectT.position = new Vector3(rectT.position.x, newY, rectT.position.z);
            if (System.Math.Round(rectT.position.y, 2) == System.Math.Round(newPos.y, 2)) break;
            yield return null;
        }


        smoothTime = 0.4f;
        newPos = rectT.position - Vector3.up * 0.6f;

        while (true)
        {
            var newY = Mathf.SmoothDamp(rectT.position.y, newPos.y, ref velocity, smoothTime);
            rectT.position = new Vector3(rectT.position.x, newY, rectT.position.z);
            var colorAlpha = Mathf.SmoothDamp(pointsText.color.a, 0, ref velocity, smoothTime);
            pointsText.color = new Color(pointsText.color.r, pointsText.color.g, pointsText.color.b, colorAlpha);
            if (System.Math.Round(rectT.position.y, 2) == System.Math.Round(newPos.y, 2))
            {
                Destroy(newTextObj);
                break;
            }
            yield return null;
        }

    }
}
