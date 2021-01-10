using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public Text roundSecondsText;
    public Text roundMinutesText;
    public TextMeshProUGUI timerText;
    public Text roundsText;
    public Text currentRoundText;
    public Text restMinutesText;
    public Text restSecondsText;
    private float numTimer = 0;
    private int numRoundSeconds = 0;
    private int numRoundMinutes = 0;
    private int numRounds = 1;
    private float numRestTime = 0;
    private int numRestSeconds = 0;
    private int numRestMinutes = 0;
    private GameObject mainScreen;
    private GameObject timeScreen;
    public Text pauseBtnText;
    private bool isTimerActive = false;

    private void Start() 
    {
        restMinutesText.text = numRestMinutes.ToString();
        roundSecondsText.text = numRoundSeconds.ToString();
        roundMinutesText.text = numRoundMinutes.ToString();
        roundsText.text = numRounds.ToString();
        restSecondsText.text = numRestTime.ToString();
        restMinutesText.text = numRestMinutes.ToString();
        timeScreen = GameObject.Find("TimeScreen");
        mainScreen = GameObject.Find("MainScreen");
        timeScreen.gameObject.SetActive(false);
    }
    public void IncreaseRoundSeconds()
    {
        
        if(numRoundSeconds == 59)
        {
            numRoundSeconds = 0;
        }
        else
        {
            numRoundSeconds++;
        }
        roundSecondsText.text = numRoundSeconds.ToString();
    }
    public void DecreaseRoundSeconds()
    {
        if(numRoundSeconds == 0)
        {
            numRoundSeconds = 59;
        }
        else
        {
            numRoundSeconds--;
        }
        roundSecondsText.text = numRoundSeconds.ToString();
    }
    public void IncreaseRoundMinutes()
    {
        numRoundMinutes++;
        roundMinutesText.text = numRoundMinutes.ToString();
    }
    public void DecreaseRoundMinutes()
    {
        if(numRoundMinutes > 0)
        {
            numRoundMinutes--;
            roundMinutesText.text = numRoundMinutes.ToString();
        }
    }
    public void IncreaseNumRounds()
    {
        if(numRounds < 10)
        {
            numRounds++;
        }
        else
        {
            numRounds = 1;
        }
        roundsText.text = numRounds.ToString();
    }
    public void DecreaseNumRounds()
    {
        if(numRounds > 1)
        {
            numRounds--;
        }
        else
        {
            numRounds = 10;
        }
        roundsText.text = numRounds.ToString();
    }
    public void IncreaseRestSeconds()
    {
        if(numRestSeconds < 59)
        {
            numRestSeconds++;
        }
        else
        {
            numRestSeconds = 0;
        }
        restSecondsText.text = numRestSeconds.ToString();

    }
    public void DecreaseRestSeconds()
    {
        if(numRestSeconds > 0)
        {
            numRestSeconds--;
        }
        else
        {
            numRestSeconds = 59;
        }
        restSecondsText.text = numRestSeconds.ToString();
    }
    public void IncreaseRestMinutes()
    {
        numRestMinutes++;
        restMinutesText.text = numRestMinutes.ToString();
    }
    public void DecreaseRestMinutes()
    {
        if(numRestMinutes > 0)
        {
            numRestMinutes--;
            restMinutesText.text = numRestMinutes.ToString();
        }
    }
    public void StartTimer()
    {
        mainScreen.gameObject.SetActive(false);
        timeScreen.gameObject.SetActive(true);
        numTimer = (numRoundMinutes * 60) + numRoundSeconds;
        numRestTime = (numRestMinutes * 60) + numRestSeconds;
        StartCoroutine(CountDownTimer());
        isTimerActive = true;
    }
    IEnumerator CountDownTimer()
    {
        int currentRoundNum = 1;
        float newNumTimer = numTimer;
        float newRestTime = numRestTime;
        int i;
        int j;
            while(currentRoundNum <= numRounds)
            {
               newNumTimer = numTimer;
                i = -numRoundSeconds;
               newRestTime = numRestTime;
                j = -numRestSeconds;

                while(newNumTimer > 0)
                {
                    while(isTimerActive == false)
                    {
                        yield return null;
                    }
                    currentRoundText.text = currentRoundNum.ToString();
                    timerText.color = new Color32(0,255,0,255);
                    DisplayTime(newNumTimer);
                    yield return new WaitForSeconds(1f);
                    newNumTimer--;
                    i++;
                }
                RunAlarm();
                if(currentRoundNum != numRounds)
                {
                    while(newRestTime > 0)
                    {
                        while(isTimerActive == false)
                        {
                            yield return null;
                        }
                        timerText.color = new Color32(254,224,0,255);
                        DisplayTime(newRestTime);
                        yield return new WaitForSeconds(1f);
                        newRestTime--;
                        j++;
                    }
                }
                RunAlarm();
                currentRoundNum++;
            }
            StopTimer();
    }
    public void StopTimer()
    {
        StopAllCoroutines();
        timeScreen.gameObject.SetActive(false);
        mainScreen.gameObject.SetActive(true);      
    }
    private void RunAlarm()
    {
        GetComponent<AudioSource>().Play();
    }
    public void TimerButton()
    {
        isTimerActive = !isTimerActive;
        pauseBtnText.text = isTimerActive ? "Pause" : "Start";
        Debug.Log(isTimerActive);
    }
    private void DisplayTime(float timeToDisplay)
    {
        // timeToDisplay +=1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}