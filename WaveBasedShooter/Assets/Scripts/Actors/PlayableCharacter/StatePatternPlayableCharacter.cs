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

    private void Start()
    {
        pcAnimator = this.GetComponent<Animator>();

        //Get States
        idleState = new PCIdleState(this);
        walkState = new PCWalkState(this);
        sprintState = new PCSprintState(this);
        slowRollState = new PCSlowRollState(this);
        fastRollState = new PCFastRollState(this);

        //Set Starting State
        currentState = idleState;
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
