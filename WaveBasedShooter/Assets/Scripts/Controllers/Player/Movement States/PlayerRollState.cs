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
        //Check if we Can Roll
        if(!canRoll)
        {

        }

        player.PCAnimator.SetTrigger("Roll");
        player.PCController.enabled = false;

        startPosition = player.transform.position;

        //Work out Target Position        
        endPosition = startPosition + (player.transform.forward * distanceAheadofPlayer); 
        timeStartedRolling = Time.time;
        rollingState = RollingState.started;
       
    }

    private void RollPlayer()
    {
        float timeSinceStarted = Time.time - timeStartedRolling;
        float percentageComplete = timeSinceStarted / 0.75f;

        Vector3 newPos = Vector3.Lerp(startPosition, endPosition, percentageComplete);

        player.transform.position = newPos;

        if(percentageComplete >= 1.0f)
        {
            rollingState = RollingState.init;
            player.PCController.enabled = true;
          

            if (PCStateController.InputDirection == Vector3.zero)
            {
                ToIdleState();
                player.PCAnimator.SetTrigger("Idle");
            }
            else
            {
                ToWalkState();
                //player.PCAnimator.SetTrigger("Walk");

            }
        }
        
    }

    
}
