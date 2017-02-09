using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : IPlayerState
{
    private readonly PCStateController player;
    public PlayerRollState(PCStateController pcStateController)
    {
        player = pcStateController;
    }

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float timeStartedRolling;

    private float distanceAheadofPlayer = 4f;

    private RollingState rollingState = RollingState.init;
    private enum RollingState
    {
        init,
        started,
    }

    private float rollingCooldown;
    private bool canRoll;

    private void Start()
    {
        rollingCooldown = player.RollingCooldown;
    }

    public void UpdateState()
    {
        if (rollingState == RollingState.init)
            InitiateRoll();
        else
        {
           RollPlayer();
        }

    }	

    public void ToWalkState()
    {
        player.currentState = player.walkState;
        
    }

    public void ToIdleState()
    {
        player.currentState = player.idleState;
    }

    public void ToRollState()
    {
        //Ignore
    }

    private void InitiateRoll()
    {
        player.PCAnimator.SetTrigger("Roll");
        rollingState = RollingState.started;

    }

    private void RollPlayer()
    {
        player.PCAnimator.ResetTrigger("Roll");

        if (PCStateController.InputDirection == Vector3.zero)
        {
            ToIdleState();
        }
        else
        {
            ToWalkState();           
        }


        rollingState = RollingState.init;
    }

    
}
