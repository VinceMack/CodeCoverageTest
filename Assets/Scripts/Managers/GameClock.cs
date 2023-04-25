using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   This is a GameClock that frequently adjusts
 *   pawn hunger and bush berry count
 *   
*/

public class GameClock : MonoBehaviour
{
    [SerializeField] private float FREQUENCY = 1f;
    //[SerializeField] private int HUNGER_DECREMENT = 1;
    [SerializeField] private int BERRY_INCREMENT = 1;
    [SerializeField] private int TREE_INCREMENT = 1;
    [SerializeField] private int WHEAT_INCREMENT = 1;
    private bool paused = true;
    private Coroutine coroutine;

    private float resumeDelay = 0f;
    private float lastTick = 0f;
    
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        CancelInvoke("OnTick");
        paused = false;
        InvokeRepeating("OnTick", FREQUENCY, FREQUENCY);
    }

    // Pause the GameClock
    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            resumeDelay = FREQUENCY - Time.time + lastTick;
            CancelInvoke("OnTick");
        }
    }

    // Resume the GameClock
    public void Resume()
    {
        if(paused)
        {
            paused = false;
            InvokeRepeating("OnTick", resumeDelay, FREQUENCY);
        }
    }

    // Executes functions every tick
    public void OnTick()
    {
        if(FREQUENCY == 0){
            return;
        }

        lastTick = Time.time;
        //Pawn_VM.DecrementHunger(HUNGER_DECREMENT);
        Bush.IncrementAllResources(BERRY_INCREMENT);
        Tree.IncrementAllResources(TREE_INCREMENT);
        Wheat.IncrementAllResources(WHEAT_INCREMENT);
    }
}
