using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour {

    // Use this for initialization
   
    [SerializeField]
    GameObject timeLimitPanel;

    [SerializeField]
    Text timeText;
    [SerializeField]
    Text m_MessageText;

    [SerializeField]
   private GameObject round1;
   
  
    private bool roundOneDone = false;

    public float TimeLeft
    {
        get
        {
            return timeLeft;
        }
        set
        {
            timeLeft = value;
        }
    }
    public bool Round1
    {
        get
        {
            return round1.activeSelf;
        }
    }
  
    float timeLeft = 60;
    float timeAdded;
    public void TimeUP()
    {
        timeAdded = UnityEngine.Random.Range(5, 20);
        timeLeft += timeAdded;
        Debug.Log("Current Time: " + timeLeft + "\n" + "Time Added: " + timeAdded);
        
    } 
    void start()
    {      
        timeText = GetComponent<UnityEngine.UI.Text>();
                   
    }   
    void Update()
    {      
        TimeLeft -= Time.deltaTime;
        timeText.text = ((int)TimeLeft).ToString();

        // Debug.Log("Time Left: " + timeLeft);
        if(TimeLeft >= 100)
        {
            timeText.text = "∞";
        }
        if (TimeLeft <= 30)
        {
            float warningMessageInterval = 2.5f;
          
            warningMessageInterval -= Time.deltaTime;
            m_MessageText.text = "Hurry up!";
            if (warningMessageInterval <= 0)
            {
             m_MessageText.text = "";
            }        
        }
        checkWinner();
       
      
        if(TimeLeft <= 0)
        {
            timeText.text = "0";
            GameOver();
        }
    }

    private void checkWinner()
    {
        if (Round1 == false && roundOneDone == false)
        {
            TimeLeft += 30;
            roundOneDone = true;
        }
    
    }

    private void GameOver()
    {
        m_MessageText.text = "Time Up!";        
    }
}
