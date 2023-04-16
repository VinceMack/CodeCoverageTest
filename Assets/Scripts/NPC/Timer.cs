using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   This is a timer that frequently adjusts
 *   pawn hunger and bush berry count
 *   
*/

public class Timer : MonoBehaviour
{
    [SerializeField] private float FREQUENCY = 1f;
    [SerializeField] private int HUNGER_DECREMENT = 1;
    [SerializeField] private int BERRY_INCREMENT = 1;
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

    // Pause the timer
    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            resumeDelay = FREQUENCY - Time.time + lastTick;
            CancelInvoke("OnTick");
        }
    }

    // Resume the timer
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
        lastTick = Time.time;
        Pawn_VM.DecrementHunger(HUNGER_DECREMENT);
        Bush.incrementBerries(BERRY_INCREMENT);
        Wheat.growWheat(WHEAT_INCREMENT);
    }
}
