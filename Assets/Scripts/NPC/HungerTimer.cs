using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *   This is a timer that frequently decrements
 *   pawn hunger when added to the scene
 *
 *   Pawns die when hunger hits zero
*/

public class HungerTimer : MonoBehaviour
{
    private float FREQUENCY = 1f;
    private int decrement = 1;
    private float resumeDelay = 0f;
    private float lastTick = 0f;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DecrementHunger", 0f, FREQUENCY);
    }

    // Pause the timer
    public void Pause()
    {
        resumeDelay = FREQUENCY - Time.time + lastTick;
        CancelInvoke("DecrementHunger");
    }

    // Resume the timer
    public void Resume()
    {
        InvokeRepeating("DecrementHunger", resumeDelay, FREQUENCY);
    }

    // Decrement the hunger for all living pawns
    public void DecrementHunger()
    {
        lastTick = Time.time;
        for(int i=Pawn.PawnList.Count-1; i>=0; i--)
        {
            Pawn.PawnList[i].hunger -= decrement;
            if(Pawn.PawnList[i].hunger <= 0)
            {
                Pawn.PawnList[i].hunger = 0;
                Pawn.PawnList[i].Die("has starved to death.");
            }
        }
    }
}
