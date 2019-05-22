using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameTimer : GameSystem
{
    int hours;
    int minutes;
    int seconds;

    public int Seconds { get => seconds; set
        {
            if (seconds == 60)
            {
                seconds = 0;
                Minutes++;
            }
            else seconds = value;
        } }

    public int Minutes { get => minutes;
        set {
            if(minutes == 60)
            {
                minutes = 0;
                Hours++;
            }
            else minutes = value;
        }
    }

    public int Hours { get => hours; set => hours = value; }

    public event System.Action<int, int, int> timerEvent = null;

    //UI
    [SerializeField] GameObject timeGO;
    [SerializeField] GameObject timeLabelGO;
    Text timerText;

    IEnumerator timerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        timerText = timeGO.GetComponent<Text>();
        timerText.text = "";
        //StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextActive(bool isActive)
    {
        timeGO.SetActive(isActive);
        timeLabelGO.SetActive(isActive);
    }

    public void StartTimer()
    {
        StopTimer();
        timerCoroutine = TimerCoroutine();
        StartCoroutine(timerCoroutine);
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void ResetTimer()
    {
        seconds = 0;
        minutes = 0;
        hours = 0;
    }

    IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Seconds++;
            timerEvent?.Invoke(Hours, Minutes, Seconds);
            timerText.text = seconds.ToString();
        }
    }

}
