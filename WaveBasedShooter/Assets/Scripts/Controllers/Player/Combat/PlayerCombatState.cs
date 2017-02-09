using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : BaseMonoBehaviour
{
    [SerializeField]
    private Animator pcAnimator;

    private bool allowInput = false;

    int combatCount = 0;
    int maxCombatCount = 1;


    public IEnumerator BeginStandardAttacks()
    {
        //First Attack
        allowInput = true;
        combatCount = 1;
        maxCombatCount = 2;
        pcAnimator.SetBool("isAttacking", true);
        pcAnimator.SetInteger("Attack Count", combatCount);
        yield return StartCoroutine(AttackCooldown(1f));

        if (combatCount < maxCombatCount)
        {
            ReturnToIdle();
        }
        else
        {
            maxCombatCount = 3;
            pcAnimator.SetInteger("Attack Count", combatCount);
            yield return StartCoroutine(AttackCooldown(1.5f));   

            if (combatCount < maxCombatCount)
            {
                ReturnToIdle();
            }
            else
            {
                Debug.Log("Three");
                pcAnimator.SetInteger("Attack Count", combatCount);
                yield return StartCoroutine(AttackCooldown(1.75f));
                ReturnToIdle();
            }
        }
    }

    public IEnumerator JumpAttack()
    {
        Debug.Log("Started");
        pcAnimator.SetBool("isAttacking", true);
        yield return StartCoroutine(AttackCooldown(3f));
        ReturnToIdle();
        Debug.Log("Finsihed");
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
        if (allowInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                combatCount++;
                pcAnimator.SetInteger("Attack Count", combatCount);
                //allowInput = false;
            } 
        }
    }

    private void ReturnToIdle()
    {
        //Return to Idle
        allowInput = false;
        combatCount = 0;
        maxCombatCount = 1;
        pcAnimator.SetBool("isAttacking", false);
        pcAnimator.SetInteger("Attack Count", combatCount);
        EventManager.TriggerEvent(Events.ReturnToIdle);
    }
}
