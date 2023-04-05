using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   This is a timer that frequently decrements
 *   pawn hunger when added to the scene
 *
 *   Pawns die when hunger hits zero
*/

public class Timer : MonoBehaviour
{
    private const float FREQUENCY = 1f;
    private const int HUNGER_DECREMENT = 1;
    private const int BERRY_INCREMENT = 1;

    private float resumeDelay = 0f;
    private float lastTick = 0f;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("OnTick", 0f, FREQUENCY);
    }

    // Pause the timer
    public void Pause()
    {
        resumeDelay = FREQUENCY - Time.time + lastTick;
        CancelInvoke("OnTick");
    }

    // Resume the timer
    public void Resume()
    {
        InvokeRepeating("OnTick", resumeDelay, FREQUENCY);
    }

    // Executes functions every tick
    public void OnTick()
    {
        lastTick = Time.time;
        Pawn.decrementHunger(HUNGER_DECREMENT);
        Bush.incrementBerries(BERRY_INCREMENT);
    }
}
