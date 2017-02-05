using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private readonly PCStateController player;
    public PlayerIdleState(PCStateController pcStateController)
    {
        player = pcStateController;
    }
    
    public void UpdateState()
    {
        CheckForMovement();
    }

    public void ToIdleState()
    {

    }

    public void ToWalkState()
    {        
        player.currentState = player.walkState;
    }

    public void ToRollState()
    {

    }    

    private void CheckForMovement()
    {       
        if (PCStateController.InputDirection != Vector3.zero)
            ToWalkState();
    }
    




}
