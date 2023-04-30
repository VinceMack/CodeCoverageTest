using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnimatorController : MonoBehaviour
{
    private Animator anim;
    private static int currentAnimatorIndex = 0; // Add this static variable

    void Awake()
    {
        anim = GetComponent<Animator>();
        RuntimeAnimatorController[] animators = Resources.LoadAll<RuntimeAnimatorController>("Animations/Animators");

        // Assign the animator at the current index
        anim.runtimeAnimatorController = animators[currentAnimatorIndex];

        // Increment the index and reset it if it reaches the end of the array
        currentAnimatorIndex++;
        if (currentAnimatorIndex >= animators.Length)
        {
            currentAnimatorIndex = 0;
        }
    }

    public void SetAnimParameter(string floatName, float floatValue)
    {
        anim.SetFloat(floatName, floatValue);
    }

    public void SetAnimParameter(string boolName, bool boolValue)
    {
        anim.SetBool(boolName, boolValue);
    }

    public float GetAnimFloat(string floatName)
    {
        return anim.GetFloat(floatName);
    }

    public bool GetAnimBool(string boolName)
    {
        return anim.GetBool(boolName);
    }

    public void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimParameter("xMovement", 1f);
            }
            else if (direction.x < 0)
            {
                SetAnimParameter("xMovement", -1f);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimParameter("yMovement", 1f);
            }
            else if (direction.y < 0)
            {
                SetAnimParameter("yMovement", -1f);
            }
        }
    }
}



