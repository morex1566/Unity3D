using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerBaseController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    public CharacterController CharacterController
    {
        get => characterController;
    }

    [SerializeField] private Animator animator;
    public Animator Animator
    {
        get => animator;
    }

    [SerializeField] private PlayerData dataPrefab;
    public PlayerData DataPrefab
    {
        get => dataPrefab;
    }

    [SerializeField] private GameObject playerCameraPrefab;
    public GameObject PlayerCameraPrefab
    {
        get => playerCameraPrefab;
    }

    private GameObject playerCameraInstance;
    public GameObject PlayerCameraInstance
    {
        get => playerCameraInstance;
    }

    private PlayerCameraController cameraController;
    public PlayerCameraController CameraController
    {
        get => cameraController;
    }

    private PlayerData dataInstance;
    public PlayerData DataInstance
    {
        get => dataInstance;
    }

    private PlayerState state;
    public PlayerState State
    {
        get => state;
        set => state = value;
    }

    /// <summary>
    /// WASD 입력값
    /// </summary>
    private Vector2 moveInput;
    public Vector2 MoveInput
    {
        get => moveInput;
        set => moveInput = value;
    }

    /// <summary>
    /// forward 벡터와 유사
    /// </summary>
    private Vector3 moveDirection;
    public Vector3 MoveDirection
    {
        get => moveDirection;
        set => moveDirection = value;
    }

    private Vector3 moveRelativeDirection;
    public Vector3 MoveRelativeDirection
    {
        get => moveRelativeDirection;
        set => moveRelativeDirection = value;
    }

    /// <summary>
    /// Move Direction * Move Speed
    /// </summary>
    private Vector3 moveVelocity;
    public Vector3 MoveVelocity
    {
        get => moveVelocity;
        set => moveVelocity = value;
    }

    /// <summary>
    /// Input * Move Speed
    /// </summary>
    private Vector3 moveRelativeVelocity;
    public Vector3 MoveRelativeVelocity
    {
        get => moveRelativeVelocity;
        set => moveRelativeVelocity = value;
    }

    private float rotationAngle;
    public float RotationAngle
    {
        get => rotationAngle;
        set => rotationAngle = value;
    }

    private float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    private bool isInCombat;
    public bool IsInCombat
    {
        get => isInCombat;
        set => isInCombat = value;
    }



    private void Awake()
    {
        moveInput = Vector2.zero;
        moveDirection = transform.forward;
        moveRelativeDirection = transform.forward;
        moveVelocity = Vector3.zero;
        moveRelativeVelocity = Vector3.zero;
        moveSpeed = 0f;
        rotationAngle = 0f;
        IsInCombat = false;
    }

    private void Start()
    {
        dataInstance = Instantiate(dataPrefab);

        // 카메라 초기화
        playerCameraInstance = Instantiate(playerCameraPrefab);
        cameraController = playerCameraInstance.GetComponent<PlayerCameraController>();
        cameraController.PlayerInstance = gameObject;
        cameraController.BaseController = this;

        // 상태 초기화
        state = PlayerState.CreateState<PlayerNonCombatIdleState>(this);
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();

        RotateCharacter();
        MoveCharacter();
    }

    private void Update()
    {
        state.Update();

        SetAnimationParameters();
    }

    private void LateUpdate()
    {
        state.LateUpdate();
    }

    private void MoveCharacter()
    {
        characterController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void RotateCharacter()
    {
        transform.Rotate(Vector3.down, rotationAngle);
    }

    private void SetAnimationParameters()
    {
        animator.SetFloat(PlayerAnimationParameter.MoveInputX, moveInput.x);
        animator.SetFloat(PlayerAnimationParameter.MoveInputY, moveInput.y);

        animator.SetFloat(PlayerAnimationParameter.MoveDirectionX, moveDirection.x);
        animator.SetFloat(PlayerAnimationParameter.MoveDirectionY, moveDirection.z);

        animator.SetFloat(PlayerAnimationParameter.MoveRelativeDirectionX, moveRelativeDirection.x);
        animator.SetFloat(PlayerAnimationParameter.MoveRelativeDirectionY, moveRelativeDirection.z);

        animator.SetFloat(PlayerAnimationParameter.MoveVelocityX, moveVelocity.x);
        animator.SetFloat(PlayerAnimationParameter.MoveVelocityY, moveVelocity.z);

        animator.SetFloat(PlayerAnimationParameter.MoveRelativeVelocityX, MoveRelativeVelocity.x);
        animator.SetFloat(PlayerAnimationParameter.MoveRelativeVelocityY, moveRelativeVelocity.z);

        animator.SetFloat(PlayerAnimationParameter.RotationAngle, rotationAngle);

        animator.SetFloat(PlayerAnimationParameter.MoveSpeed, moveSpeed);

        animator.SetBool(PlayerAnimationParameter.IsInCombat, isInCombat);
    }
}
