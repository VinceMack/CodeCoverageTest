using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorController))]
[RequireComponent(typeof(StateMachine))]
public class ControlledMovement : Movement
{
    private Vector3 change;
    private StateMachine stateMachine;
    private AnimatorController anim;

    // Start is called before the first frame update
    public override void Awake()
    {
        anim = GetComponent<AnimatorController>();
        stateMachine = GetComponent<StateMachine>();
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change != Vector3.zero)
        {
            stateMachine.ChangeState(GenericState.walk);
            anim.ChangeAnim(change);
            anim.SetAnimParameter("walking", true);
        }
        else
        {
            anim.SetAnimParameter("walking", false);
        }
        base.Motion(change);
    }
}




