using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : BaseMonoBehaviour
{
    [SerializeField]
    private Animator pcAnimator;

    private bool allowInput;

    int combatCount = 0;


    public IEnumerator BeginStandardAttacks()
    {
        //First Attack
        allowInput = false;
        pcAnimator.SetTrigger("Standard Attack 1");
        yield return StartCoroutine(AttackCooldown(0.5f));

        //Wait for Second Attack Input
        allowInput = true;
        yield return StartCoroutine(AttackCooldown(0.5f));

        if (combatCount == 0)
        {
            ReturnToIdle();
        }
        else
        {
            pcAnimator.SetTrigger("Standard Attack 2");
            yield return StartCoroutine(AttackCooldown(0.5f));

            allowInput = true;
            yield return StartCoroutine(AttackCooldown(0.5f));

            if (combatCount == 1)
            {
                ReturnToIdle();
            }
            else
            {
                pcAnimator.SetTrigger("Standard Attack 3");
                yield return StartCoroutine(AttackCooldown(1f));

                combatCount = 0;
                allowInput = false;

                ReturnToIdle();
            }
        }
            



       
    }

    private IEnumerator AttackCooldown(float timer)
    {
        yield return new WaitForSeconds(timer);
    }

    public override void UpdateNormal()
    {
        CheckForAttackInput();
    }

    private void CheckForAttackInput()
    {
        if(allowInput)
        {
            if (Input.GetMouseButton(0))
            {
                combatCount++;
                allowInput = false;
            } 
        }
    }

    private void ReturnToIdle()
    {
        //Return to Idle
        EventManager.TriggerEvent(Events.ReturnToIdle);
    }
}
