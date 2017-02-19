using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternPlayableCharacter : BaseMonoBehaviour
{
    //Components
    private Animator pcAnimator;
    public Animator PCAnimator
    {
        get
        {
            return pcAnimator;
        }
    }

    [Header("Actions")]
    [SerializeField]
    private MovementActions sprintAction;
    public MovementActions SprintAction
    {
        get
        {
            return sprintAction;
        }
    }
    [SerializeField]
    private MovementActions slowRollAction;
    public MovementActions SlowRollAction
    {
        get
        {
            return slowRollAction;
        }
    }
    [SerializeField]
    private MovementActions fastRollAction;
    public MovementActions FastRolLAction
    {
        get
        {
            return fastRollAction;
        }
    }

    //Weapons========================================================================
    [Header("Weapons")]
    [SerializeField]
    private WeaponData startingWeapon;
    private WeaponData currentWeapon;
    public WeaponData CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }
    }
    private static WeaponPickup weaponPickup;
    public static WeaponPickup WeaponPickUp
    {
        get
        {
            return weaponPickup;
        }
        set
        {
            weaponPickup = value;
        }
    }

    [Header("Shields")]
    [SerializeField]
    private GameObject shieldObject;
    private static ShieldHandler shield;
    public static ShieldHandler Shield
    {
        get
        {
            return shield;
        }
        set
        {
            shield = value;
        }
    }
    private bool hasShield;
    public bool HasShield
    {
        get
        {
            return hasShield;
        }
    }

    //States=========================================================================

    //Core States
    private IPlayableCharacterState currentState;
    public IPlayableCharacterState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }
    private IPlayableCharacterState lastState;

    //States
    [HideInInspector] public PCIdleState idleState;
    [HideInInspector] public PCWalkState walkState;
    [HideInInspector] public PCSprintState sprintState;
    [HideInInspector] public PCSlowRollState slowRollState;
    [HideInInspector] public PCFastRollState fastRollState;

    //Inputs =========================================================================
    //Movement
    private Vector3 movementVector;
    public Vector3 MovementVector
    {
        get
        {
            return movementVector;
        }
    }

    private bool isSprinting;
    public bool IsSprinting
    {
        get
        {
            return isSprinting;
        }
    }

    private bool isRolling;
    public bool IsRolling
    {
        get
        {
            return isRolling;
        }
    }

    private bool isAttacking;
    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }
    }

    private void Start()
    {
        pcAnimator = this.GetComponent<Animator>();

        //Get States
        idleState = new PCIdleState(this);
        walkState = new PCWalkState(this);
        sprintState = new PCSprintState(this);
        slowRollState = new PCSlowRollState(this);
        fastRollState = new PCFastRollState(this);

        //Weapon
        GetWeapon();

        //Set Starting State
        currentState = idleState;
    }
    /// <summary>
    /// Pickups Shield
    /// Shield Variable is set in ShieldHandler
    /// </summary>
    void SetShield()
    {
        shield.PickUpItem();
        
        hasShield = true;
        pcAnimator.SetBool("hasShield", true);

        shieldObject.SetActive(true);

        shield = null;
    }
    
    void GetWeapon()
    {
        //currentWeapon = we;        

        switch(currentWeapon.WeaponType)
        {
            case WeaponType.Type.Fists:
                pcAnimator.SetBool("hasWeapon", false);
                pcAnimator.SetBool("isSingleHandedWeapon", false);
                pcAnimator.SetBool("isDoubleHandedWeapon", false);
                break;

            case WeaponType.Type.SingleHanded:
                pcAnimator.SetBool("hasWeapon", true);
                pcAnimator.SetBool("isSingleHandedWeapon", true);
                pcAnimator.SetBool("isDoubleHandedWeapon", false);
                Debug.Log("Picked up Single Handed Weapon");
                break;

            case WeaponType.Type.DoubleHanded:
                pcAnimator.SetBool("hasWeapon", true);
                pcAnimator.SetBool("isSingleHandedWeapon", false);
                pcAnimator.SetBool("isDoubleHandedWeapon", true);
                Debug.Log("Picked up Double Handed Weapon");
                break;
        }
    }

    public override void UpdateNormal()
    {
        GetInputs();

        UpdateCurrentState();
    }

    private void UpdateCurrentState()
    {
        if(lastState != currentState)
        {
            if (lastState != null)
                lastState.OnUpdateState();

            lastState = currentState;

            Debug.Log(currentState);
        }
        else
        {
            currentState.OnUpdateState();
        }
    }

    private void GetInputs()
    {
        //PickUp
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (shield)
                SetShield();

            
        }


        //Get Combat Input & Check We're not Rolling
        if(!isRolling)
        {
            if(Input.GetMouseButton(0))
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }
        }
       

        //Get Roll Input
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isRolling = true;
        }
        else
        {
            isRolling = false;
        }

        //Get Sprint Input
        if(Input.GetMouseButton(1))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        //Get Directional Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movementVector = new Vector3(x, 0, z);
    }
}
