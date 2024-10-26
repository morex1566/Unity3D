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

    private Vector2 moveInput;
    public Vector2 MoveInput
    {
        get => moveInput;
        set => moveInput = value;
    }

    private Vector3 moveDirection;
    public Vector3 MoveDirection
    {
        get => moveDirection;
        set => moveDirection = value;
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
        rotationAngle = 0f;
        moveSpeed = 0f;
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
        animator.SetBool(PlayerAnimationParameter.IsInCombat, isInCombat);

        animator.SetFloat(PlayerAnimationParameter.MoveInputX, moveInput.x);
        animator.SetFloat(PlayerAnimationParameter.MoveInputY, moveInput.y);
        animator.SetFloat(PlayerAnimationParameter.MoveDirectionX, moveDirection.x);
        animator.SetFloat(PlayerAnimationParameter.MoveDirectionY, moveDirection.z);
        animator.SetFloat(PlayerAnimationParameter.MoveSpeed, moveSpeed);
        animator.SetFloat(PlayerAnimationParameter.RotationAngle, rotationAngle);
    }
}
