using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IPlayerState
{
    private readonly PCStateController player;
    public PlayerWalkState(PCStateController pcStateController)
    {
        player = pcStateController;
    }

    private Vector3 inputDirection;

	public void UpdateState()
    {
        MovePlayableCharacter();
    }    

    public void ToIdleState()
    {
       player.currentState = player.idleState;
    }

    public void ToWalkState()
    {
        //Ignore
    }

    public void ToRollState()
    {
        player.currentState = player.rollState;
    }

    public void ToAttackMode()
    {
        player.currentState = player.standardAttackState;
    }

    private void MovePlayableCharacter()
    {
        //Check for Rolling
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToRollState();
            return;
        }

        //Check For Attack
        if (Input.GetMouseButtonDown(0))
        {
            ToAttackMode();
            return;
        }


        if (PCStateController.InputDirection == Vector3.zero)
        {
            ToIdleState();
        }
        else
        {
            float speed = player.MovementSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 6f;
            }

            Vector3 movementDirection = PCStateController.InputDirection * speed;

            if (movementDirection != Vector3.zero)
                player.transform.rotation = Quaternion.LookRotation(movementDirection);

            player.PCController.Move(movementDirection * Time.deltaTime);
        }
    }
}
