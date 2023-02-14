using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] public Animator anim;

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