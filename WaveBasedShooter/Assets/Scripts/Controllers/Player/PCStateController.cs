using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStateController : BaseMonoBehaviour
{
    //Components
    private CharacterController pcController;
    public CharacterController PCController
    {
        get
        {
            return pcController;
        }
    }

    [SerializeField]
    private PlayerCombatState combatState;

    [SerializeField]
    private Animator pcAnimator;
    public Animator PCAnimator
    {
        get
        {
            return pcAnimator;
        }
    }

    [Header("Movement Attributes")]
    [SerializeField]
    private float movementSpeed = 5;
    public float MovementSpeed { get { return movementSpeed; } }
    private static Vector3 inputDirection;
    public static Vector3 InputDirection
    {
        get
        {
            return inputDirection;
        }
    }

    [Header("Rolling Cooldown")]
    [SerializeField]
    private float rollingCooldown = 0.5f;
    public float RollingCooldown
    {
        get
        {
            return rollingCooldown;
        }
    }

    //States
    public IPlayerState currentState;
    public PlayerWalkState walkState;  
    public PlayerIdleState idleState;
    public PlayerRollState rollState;
    public PlayerStandardAttackState standardAttackState;

    private PlayerState playerState;
    private enum PlayerState
    {
        Combat,
        Moving
    }   

    protected override void Awake()
    {
        base.Awake();

        walkState = new PlayerWalkState(this);
        idleState = new PlayerIdleState(this);
        rollState = new PlayerRollState(this);
        standardAttackState = new PlayerStandardAttackState(this);     
    }

    private void OnEnable()
    {
        EventManager.StartListening(Events.ReturnToIdle, FinishCombatRound);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.ReturnToIdle, FinishCombatRound);
    }

    // Use this for initialization
    void Start()
    {
        Init();
    }

    void Init()
    {
        playerState = PlayerState.Moving;
        pcController = this.GetComponent<CharacterController>();
        currentState = idleState;
    }

    public override void UpdateNormal()
    {
        if (playerState == PlayerState.Combat)
            return;

        currentState.UpdateState();
        GetInput();
    }

    public void GetInput()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    currentState = rollState;
        //}

        if(Input.GetMouseButtonDown(0))
        {
            if(currentState != rollState)
            {
                StartCoroutine(combatState.BeginStandardAttacks());
                playerState = PlayerState.Combat;
            }
           
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            pcAnimator.SetBool("isSprinting", true);
        }
        else
        {
            pcAnimator.SetBool("isSprinting", false);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        inputDirection = new Vector3(x, 0, z);

        if(inputDirection == Vector3.zero)
        {
            pcAnimator.SetFloat("movement",0);
        }
        else
        {
            pcAnimator.SetFloat("movement", 1);
        }
    }

    private void FinishCombatRound()
    {
        playerState = PlayerState.Moving;
        currentState = idleState;
        //pcAnimator.SetTrigger("Idle");
    }
}
