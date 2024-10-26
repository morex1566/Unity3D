using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private PlayerBaseController baseController;
    public PlayerBaseController BaseController
    {
        get { return baseController; }
        set { baseController = value; }
    }

    [SerializeField] private Camera cam;
    public Camera Cam
    {
        get { return cam; }
        set { cam = value; }
    }

    private GameObject camArmInst;

    private GameObject camArmPivotInst;

    private InputAction looking;

    private InputAction moving;

    private Vector3 nonCombatCamArmPivotPos;
    public Vector3 NonCombatCamArmPivotPos
    {
        get { return nonCombatCamArmPivotPos; }
    }

    private Vector3 combatCamArmPivotPos;
    public Vector3 CombatCamArmPivotPos
    {
        get { return combatCamArmPivotPos; }
    }

    // distance from pivot to cam at non-combat state
    private float nonCombatCamDist;
    public float NonCombatCamDist
    {
        get { return nonCombatCamDist; }
        set { nonCombatCamDist = value; }
    }

    // distance from pivot to cam at combat state
    private float combatCamDist;
    public float CombatCamDist
    {
        get { return combatCamDist; }
        set { combatCamDist = value; }
    }

    // default cam fov
    private float camFOV;
    public float CamFOV
    {
        get { return camFOV; }
        set 
        { 
            camFOV = value;
            cam.fieldOfView = value;
        }
    }

    // cam fov lerp weight
    private float camFOVLerpInteria;
    public float CamFOVLerpInteria
    {
        get { return camFOVLerpInteria; }
        set { camFOVLerpInteria = value; }
    }

    // rotation clamp
    private float minRotationX;
    private float maxRotationX;

    // mouse input for using tps imple
    private Vector2 camLookingDelta;
    public Vector2 CamLookingDelta
    {
        get { return camLookingDelta; }
    }

    // keyboard input for using tps imple
    private Vector3 camMovingDelta;
    public Vector3 CamMovingDelta
    {
        get { return camMovingDelta; }
    }

    // is cam fov lerping?
    private Coroutine lerpingFOV;

    // is cam pivot pos lerping?
    private Coroutine lerpingPivotPos;



    private void Awake()
    {
        cam = GetComponent<Camera>();

        looking = InputManager.InputMappingContext.Player.Look;
        moving = InputManager.InputMappingContext.Player.Move;

        nonCombatCamArmPivotPos = new Vector3(0.5f, 2f, 0);
        nonCombatCamDist = 3f;
        camFOV = 63f;
        camFOVLerpInteria = 5f;

        minRotationX = -60f;
        maxRotationX = 60f;
    }

    private void Start()
    {
        cam.fieldOfView = camFOV;

        // 카메라 암의 위치를 가리키는 인스턴스
        camArmPivotInst = new GameObject("Player Camera Arm Pivot");
        camArmPivotInst.transform.SetParent(baseController.transform);
        camArmPivotInst.transform.position = baseController.transform.position + nonCombatCamArmPivotPos;

        // 카메라 암 인스턴스
        camArmInst = new GameObject("Player Camera Arm");
        transform.SetParent(camArmInst.transform);
        MoveCamArm();
        MoveCam();
    }

    private void Update()
    {
        camLookingDelta = looking.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        MoveCamArm();
        RotateCamArm();
        RotateCam();
    }



    // using...
    // 1. when combat-state to non-combat-state
    // 2. when cam rotate around baseController, cam must be center to baseController
    public void LerpPivotPos(Vector3 newPivotPos)
    {

    }

    // using...
    // 1. when baseController running
    public void LerpFOV(float fov)
    {
        if (lerpingFOV != null)
        {
            StopCoroutine(lerpingFOV);
        }

        lerpingFOV = StartCoroutine(LerpFOVCoroutine(fov));
    }



    private void MoveCamArm()
    {
        camArmInst.transform.position = camArmPivotInst.transform.position;
    }

    private void MoveCam()
    {
        transform.position = camArmInst.transform.position + new Vector3(0, 0, -nonCombatCamDist);
    }

    private void RotateCamArm()
    {
        // 마우스에 따른 카메라 회전
        camArmInst.transform.Rotate(Vector3.up, camLookingDelta.x * 0.2f, Space.World);
        camArmInst.transform.Rotate(Vector3.left, camLookingDelta.y * 0.15f, Space.World);

        // 카메라 회전하는데 위 아래 한도 설정 구현
        Vector3 camArmRotation = camArmInst.transform.rotation.eulerAngles;
        if (camArmRotation.x > 180f)
        {
            camArmRotation.x -= 360f;
        }
        float clampedCamArmRotationX = Mathf.Clamp(camArmRotation.x, minRotationX, maxRotationX);
        Vector3 clampedCamArmRotation = new Vector3(clampedCamArmRotationX, camArmRotation.y, camArmRotation.z);
        camArmInst.transform.rotation = Quaternion.Euler(clampedCamArmRotation);
    }

    private void RotateCam()
    {
        transform.LookAt(camArmInst.transform);
    }

    private IEnumerator LerpPivotPosCoroutine(Vector3 newPivotPos)
    {
        yield return null;
    }

    private IEnumerator LerpFOVCoroutine(float fov)
    {
        while (Mathf.Approximately(fov, cam.fieldOfView) == false)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, Time.deltaTime * CamFOVLerpInteria);
            yield return null;
        }

        lerpingFOV = null;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
        {
            return;
        }

        // cam arm pivot
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(camArmPivotInst.transform.position, 0.1f);
        Handles.Label(camArmPivotInst.transform.position, "cam_arm_pivot_pos");

        // cam distance to cam arm pivot
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, camArmPivotInst.transform.position);
        Handles.Label((camArmPivotInst.transform.position + transform.position) * 0.5f, "cam_distance_to_pivot");
    }
#endif
}
