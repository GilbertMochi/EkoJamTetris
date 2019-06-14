using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster : MonoBehaviour
{
    public static int PlayScore { get; private set; }
    public int ScoreAmountEverySecond = 1;//amount of score added to player score every second
    LevelMaster levMas;

    TimeSpan duration;
    float lastUpdate;

    public void ReduceFromPlayScore(int amount)
    {
        PlayScore -= amount;
    }

    private void Start()
    {
        levMas = GetComponent<LevelMaster>();
        duration = new TimeSpan(0, 0, LevelMaster.SecondsToAdd);
        PlayScore = 0;
    }

    private void Update()
    {
        if (Time.time - lastUpdate >= 1f)//if a second has passed since lastUpdate
        {
            PlayScore += ScoreAmountEverySecond;//adds to player score every second
            lastUpdate = Time.time;//last update is time now
        }

        if (levMas.PlayTime.TotalSeconds > duration.TotalSeconds)//everytime play time is longer than duration
        {
            PlayScore += (int)duration.TotalSeconds;//add the amount of seconds in duration to playerscore
            //Debug.Log($"Play score: {PlayScore}");

            duration += new TimeSpan(0, 0, LevelMaster.SecondsToAdd);//increase duration
            //Debug.Log($"New duration is {duration.ToString()}");

            ScoreAmountEverySecond++;
        }
    }
}
