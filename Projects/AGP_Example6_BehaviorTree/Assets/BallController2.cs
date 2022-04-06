using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using Random = UnityEngine.Random;

public enum HungryState {Hungry, Full}

public class BallController2 : MonoBehaviour
{
    private float _speed = 0.1f;
    public HungryState hungryState;

    private BehaviorTree.Tree<BallController2> _tree;

    Do<BallController2>.NodeAction nodeAction_BecomeFull;

    [SerializeField]
    ExtraController extraController;

    [SerializeField]
    Rigidbody rB;

    private void Start()
    {

        //Assign delegates
        nodeAction_BecomeFull += Action_BecomeFull;
        //You can add extra actions to the delegate
        nodeAction_BecomeFull += extraController.Action_ExtraMessage;

        //Tree
        var mouseDownTree = new Tree<BallController2>
        (
            new Sequence<BallController2>
            (
                new Condition<BallController2>(
                    isMouseDown
                ),
                new Selector<BallController2>
                (
                    new Sequence<BallController2>
                    (
                        new Not<BallController2>(
                            new Condition<BallController2>(
                                isHungryCondition
                                )
                        ),
                        new Do<BallController2>(
                            Action_GoTowardsMouseFollow
                        )
                    ),
                    new Do<BallController2>(
                        Action_GoTowardsMouseAvoid
                    )
                )

            )
        );

        var spaceBarDown = new Tree<BallController2>
        (
            new Sequence<BallController2>
            (
                new Condition<BallController2>(
                    IsKeySpaceDown
                ),
                new Do<BallController2>(
                    nodeAction_BecomeFull
                )
            )
        );

        var spaceBarNotDown = new Tree<BallController2>
        (
            new Sequence<BallController2>
            (
                new Not<BallController2>(
                    new Condition<BallController2>(
                        IsKeySpaceDown
                    )
                ),
                new Not<BallController2>(
                    new Condition<BallController2>(
                        isHungryCondition
                    )
                ),
                new Do<BallController2>(
                    Action_BecomeHungry
                )
            )
        );

        _tree = new Tree<BallController2>
        (
            new Selector<BallController2>
            (
                mouseDownTree,
                spaceBarDown,
                spaceBarNotDown,
                new Do<BallController2>(
                    Action_Shake
                )
            )
        );
    }

    private void Update()
    {
        _tree.Update(this);
    }

	private void OnDestroy()
    {
        nodeAction_BecomeFull -= Action_BecomeHungry;
        nodeAction_BecomeFull -= extraController.Action_ExtraMessage;
    }

	private void FixedUpdate()
    {
		//_tree.Update(this);

		if (hungryState == HungryState.Hungry)
		{
            //Shrinking since it's hungry
            rB.mass -= 0.01f;
            if(rB.mass < 0.05f)
			{
                rB.mass = 0.05f;
			}

        }
        else
        {
            rB.mass += 0.02f;
            if (rB.mass > 3)
            {
                rB.mass = 3;
            }

        }
        transform.localScale = new Vector3(rB.mass, rB.mass, rB.mass);
    }



	//****************For condition node****************

	private bool isHungryCondition(BallController2 context)
    {
        return hungryState == HungryState.Hungry;
    }
    private bool isMouseDown(BallController2 context)
    {
        return Input.GetMouseButton(0);
    }
    private bool IsKeySpaceDown(BallController2 context)
    {
        return Input.GetKey(KeyCode.Space);
    }







    //****************For action node****************
    public bool Action_Shake(BallController2 context)
    {
        Shake();
        return true;
    }

    public bool Action_BecomeHungry(BallController2 context)
    {
        BecomeHungry(HungryState.Hungry);
        return true;
    }
    public bool Action_BecomeFull(BallController2 context)
    {
        BecomeHungry(HungryState.Full);
        return true;
    }
    public bool Action_GoTowardsMouseFollow(BallController2 context)
    {
        GoTowardsMouse(true);
        //If set to false, Action_GoTowardsMouseAvoid() also executed
        //return false;
        return true;
    }
    public bool Action_GoTowardsMouseAvoid(BallController2 context)
    {
        GoTowardsMouse(false);
        return true;
    }





    //****************Actual Function****************
    public void Shake()
    {
        var jitter = (_speed * Random.insideUnitSphere);
        jitter.z = 0;

        transform.position += jitter;
    }

    public void GoTowardsMouse(bool towards)
    {
        var vectorTowardsMouse = (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
        vectorTowardsMouse.z = 0;

        if (towards)
        {
            transform.position -= _speed * vectorTowardsMouse;
        }
        else
        {
            transform.position += _speed * vectorTowardsMouse;
        }

    }
    public void BecomeHungry(HungryState newHungryState)
    {
        if(hungryState != newHungryState)
		{
            //OnEnter the new state
            OnEnter_HungryState(newHungryState);
            hungryState = newHungryState;
        }
    }

    public void OnEnter_HungryState(HungryState newHungryState)
	{
        Debug.Log("Enter new state: " + newHungryState);

        //animator.SetTrigger("StateChanged")
	}




}
