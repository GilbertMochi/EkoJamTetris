using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    PlayerMovement playerMovement;
    ObstacleSpawner obstacleSpawner;

    public static int SecondsToAdd { get; set; } = 40;//seconds to be added to duration once game has lasted more than duration

    private float elapsedSeconds;
    public TimeSpan PlayTime//time the player has played the the game (gamescene)
    {
        get
        {
            return TimeSpan.FromSeconds(elapsedSeconds);//gets the passed time from elapsed seconds
        }
        private set
        {
            PlayTime = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        elapsedSeconds = 0;//set the elapsed seconds to 0 at the start of the level

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        obstacleSpawner = GameObject.Find("obstacles").GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneMaster.getIsGamePaused())//if game is not paused (if game is running)
        {
            elapsedSeconds += Time.deltaTime;


            if (PlayTime.TotalSeconds > SecondsToAdd && SecondsToAdd > 5)//everytime play time is longer than duration
            {
                //increase player speed
                playerMovement.IncreaseSpeed(.4f);

                if (obstacleSpawner.SpawnTime > 1.5)
                {
                    //spawn obstacles faster
                    obstacleSpawner.SpawnTime -= .2f;
                }

                //decrease the time of the seconds to add
                SecondsToAdd -= 2;
            }
        }
    }

}