using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandardAttackState : IPlayerState
{
    private readonly PCStateController player;
    public PlayerStandardAttackState(PCStateController pcStateController)
    {
        player = pcStateController;
    }

    private float timer;

    private bool hasAttacked;

    public void UpdateState()
    {
        if(!hasAttacked)
            Attack();
    }

    public void ToWalkState()
    {

    }

    public void ToIdleState()
    {
        player.PCAnimator.SetTrigger("Idle");
        player.currentState = player.idleState;
        hasAttacked = false;
        Debug.Log("Idle");
        
    }

    public void ToRollState()
    {

    }

    public void Attack()
    {
       hasAttacked = true;

       player.PCAnimator.SetTrigger("Attack");
    }
      
}
