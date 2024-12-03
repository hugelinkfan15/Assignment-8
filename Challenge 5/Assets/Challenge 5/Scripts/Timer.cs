using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats,string> timeFormats = new Dictionary<TimerFormats,string>();

    private long minutes = 0;
    private  float seconds = 0;

    public GameManagerX gMX;
    
    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "00");
        timeFormats.Add(TimerFormats.TenthDecimal, "00.0");
        timeFormats.Add(TimerFormats.HunderethDecimal, "00.00");
        timeFormats.Add(TimerFormats.ThousandthDecimal, "00.000");
    }

    // Update is called once per frame
    void Update()
    {
        if (gMX.isGameActive)
        {
            currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
            seconds = currentTime%60;
            minutes = (int)currentTime/60;

            if (hasLimit && ((countDown && currentTime <= timerLimit)||(!countDown && currentTime >= timerLimit)))
            {
                currentTime = timerLimit;
                SetTimeText();
                timerText.color = Color.red;
                enabled = false;
                gMX.GameOver();
            }

            SetTimeText();

        }
    }

    private void SetTimeText()
    {
        timerText.text = "Time: " + seconds.ToString(timeFormats[format]);
    }

    public string GetTimerText()
    {
        return timerText.text;
    }

    public long GetMinutes()
    {
        return minutes;
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HunderethDecimal,
    ThousandthDecimal
}
