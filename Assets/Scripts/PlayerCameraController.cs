using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public Camera Cam
    {
        get => cam;
    }

    [SerializeField] private Vector3 nonCombatCamArmPivot;
    public Vector3 NonCombatCamArmPivot
    {
        get => nonCombatCamArmPivot;
        set => nonCombatCamArmPivot = value;
    }

    [SerializeField] private Vector3 combatCamArmPivot;
    public Vector3 CombatCamArmPivot
    {
        get => combatCamArmPivot;
        set => combatCamArmPivot = value;
    }

    [SerializeField] private float distance2CamArm;
    public float Distance2CamArm
    {
        get => distance2CamArm;
        set => distance2CamArm = value;
    }

    [SerializeField] private GameObject playerInstance;
    public GameObject PlayerInstance
    {
        get => playerInstance;
        set => playerInstance = value;
    }

    private PlayerBaseController baseController;
    public PlayerBaseController BaseController
    {
        get => baseController;
        set => baseController = value;
    }

    private GameObject camArmInstance;
    public GameObject CamArmInstance
    {
        get => camArmInstance;
    }

    private InputAction lookingAction;

    private Vector2 lookDelta;

    private float minRotationX;

    private float maxRotationX;



    private void Awake()
    {
        lookingAction = InputManager.InputMappingContext.Player.Look;
        lookDelta = Vector2.zero;
        minRotationX = -60f;
        maxRotationX = 60f;
    }

    private void Start()
    {
        // 카메라 암에 카메라 설치
        camArmInstance = new GameObject("Player Camera Arm");
        camArmInstance.transform.Translate(playerInstance.transform.position + nonCombatCamArmPivot, Space.World);

        transform.position = camArmInstance.transform.position;
        transform.SetParent(camArmInstance.transform);
        transform.Translate(new Vector3(0, 0, -distance2CamArm), Space.World);
        cam.depth = float.MaxValue;

        lookingAction.performed += OnLook;
        lookingAction.canceled += OnLook;
    }

    private void FixedUpdate()
    {
        MoveCamArm();
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        RotateCamArm();
        RotateCam();
    }

    private void MoveCamArm()
    {
        // 화면이 회전할 때 피봇도 같은 방향으로 회전
        Vector3 nextPos = nonCombatCamArmPivot;
        Quaternion rotationY = Quaternion.Euler(new Vector3(0f, camArmInstance.transform.rotation.eulerAngles.y, 0f));
        nextPos = rotationY * nextPos;
        nextPos += playerInstance.transform.position;

        camArmInstance.transform.position = Vector3.Lerp(camArmInstance.transform.position, nextPos, Time.deltaTime * 5f);
    }

    private void RotateCamArm()
    {
        // 마우스에 따른 카메라 회전
        camArmInstance.transform.Rotate(Vector3.up, lookDelta.x * 0.15f, Space.World);
        camArmInstance.transform.Rotate(Vector3.left, lookDelta.y * 0.15f, Space.World);

        // 카메라 회전하는데 위 아래 한도 설정 구현
        Vector3 camArmRotation = camArmInstance.transform.rotation.eulerAngles;
        if (camArmRotation.x > 180f)
        {
            camArmRotation.x -= 360f;
        }

        float clampedCamArmRotationX = Mathf.Clamp(camArmRotation.x, minRotationX, maxRotationX);
        Vector3 clampedCamArmRotation = new Vector3(clampedCamArmRotationX, camArmRotation.y, camArmRotation.z);
        camArmInstance.transform.rotation = Quaternion.Euler(clampedCamArmRotation);
    }

    private void RotateCam()
    {
        transform.LookAt(camArmInstance.transform);
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookDelta = context.ReadValue<Vector2>();
    }
}
