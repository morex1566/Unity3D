using System;
using UnityEngine;

public class PlayerBaseController : MonoBehaviour
{
    [SerializeField] private PlayerCameraController cameraController;
    public PlayerCameraController CameraController
    {
        get { return cameraController; }
        set { cameraController = value; }
    }

    [SerializeField] private CharacterController characterController;
    public CharacterController CharacterController
    {
        get { return characterController; }
        set { characterController = value; }
    }

    [SerializeField] private Animator animator;
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }

    [SerializeField] private PlayerData data;
    public PlayerData Data
    {
        get { return data; }
        set { data = value; }
    }

    private PlayerData status;
    public PlayerData Status
    {
        get { return status; }
        set { status = value; }
    }

    private PlayerState state;
    public PlayerState State
    {
        get { return state; }
        set { state = value; }
    }

    private Vector3 moveDirection;
    public Vector3 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

    private Vector2 inputDirection;
    public Vector2 InputDirection
    {
        get { return inputDirection; }
        set { inputDirection = value; }
    }

    private float moveDistance;
    public float MoveDistance
    {
        get { return moveDistance; }
        set { moveDistance = value; }
    }

    private float rotation;
    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    private bool isInCombat;
    public bool IsInCombat
    {
        get { return isInCombat; }
        set { isInCombat = value; }
    }

    private bool isWeaponDrawn;
    public bool IsWeaponDrawn
    {
        get { return isWeaponDrawn; }
        set { isWeaponDrawn = value; }
    }

    private bool onWeaponDraw;
    public bool OnWeaponDraw
    {
        get { return onWeaponDraw; }
        set { onWeaponDraw = value; }
    }



    private void Awake()
    {
        // instance
        status = Instantiate(data);
        state = new PlayerNonCombatIdleState(this);

        // constant value
        inputDirection = Vector2.zero;
        moveDirection = Vector3.zero;
        moveDistance = 0f;
        rotation = 0f;

        // boolean state
        isInCombat = false;
        isWeaponDrawn = false;

        // trigger state
        onWeaponDraw = false;
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();
    }

    private void Update()
    {
        state.Update();
    }

    private void LateUpdate()
    {
        ResetTriggers();
    }



    private void ResetTriggers()
    {
        onWeaponDraw = false;
    }
}
