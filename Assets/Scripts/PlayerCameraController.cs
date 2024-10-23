using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    [SerializeField] private Camera cam;
    public Camera Cam
    {
        get { return cam; }
        set { cam = value; }
    }

    private GameObject cameraArm;

    private InputAction looking;

    private InputAction moving;

    private Vector3 cameraArmPos;
    public Vector3 CameraArmPos
    {
        get { return cameraArmPos; }
    }

    private Vector3 nonCombatCameraArmPivotPos;
    public Vector3 NonCombatCameraArmPivotPos
    {
        get { return nonCombatCameraArmPivotPos; }
    }

    private Vector3 combatCameraArmPivotPos;
    public Vector3 CombatCameraArmPivotPos
    {
        get { return combatCameraArmPivotPos; }
    }

    // distance from pivot to cam at non-combat state
    private float nonCombatCameraDist;
    public float NonCombatCameraDist
    {
        get { return nonCombatCameraDist; }
        set { nonCombatCameraDist = value; }
    }

    // distance from pivot to cam at combat state
    private float combatCameraDist;
    public float CombatCameraDist
    {
        get { return combatCameraDist; }
        set { combatCameraDist = value; }
    }

    // default camera fov
    private float cameraFOV;
    public float CameraFOV
    {
        get { return cameraFOV; }
        set 
        { 
            cameraFOV = value;
            cam.fieldOfView = value;
        }
    }

    // camera fov lerp weight
    private float cameraFOVLerpInteria;
    public float CameraFOVLerpInteria
    {
        get { return cameraFOVLerpInteria; }
        set { cameraFOVLerpInteria = value; }
    }

    // rotation clamp
    private float minRotationX;
    private float maxRotationX;

    // mouse input for using tps imple
    private Vector2 cameraLookingDelta;
    public Vector2 CameraLookingDelta
    {
        get { return cameraLookingDelta; }
    }

    // keyboard input for using tps imple
    private Vector3 cameraMovingDelta;
    public Vector3 CameraMovingDelta
    {
        get { return cameraMovingDelta; }
    }

    // is camera fov lerping?
    private Coroutine lerpingFOV;

    // is camera pivot pos lerping?
    private Coroutine lerpingPivotPos;



    private void Awake()
    {
        looking = InputManager.InputMappingContext.Player.Look;
        moving = InputManager.InputMappingContext.Player.Move;

        cameraArmPos = Vector3.zero;

        nonCombatCameraArmPivotPos = new Vector3(0.5f, 2f, 0);
        nonCombatCameraDist = 3f;
        cameraFOV = 63f;
        cameraFOVLerpInteria = 5f;

        minRotationX = -60f;
        maxRotationX = 60f;
    }

    private void Start()
    {
        cam.fieldOfView = cameraFOV;

        // attach to target
        cameraArm = new GameObject("Player Camera Arm");
        transform.SetParent(cameraArm.transform);
        MoveCameraArm();
        MoveCamera();
    }

    private void Update()
    {
        cameraLookingDelta = looking.ReadValue<Vector2>();   
    }

    private void LateUpdate()
    {
        MoveCameraArm();
        RotateCameraArm();
        RotateCamera();
    }



    // using...
    // 1. when combat-state to non-combat-state
    // 2. when cam rotate around character, cam must be center to character
    public void LerpPivotPos(Vector3 newPivotPos)
    {

    }

    // using...
    // 1. when character running
    public void LerpFOV(float fov)
    {
        if (lerpingFOV != null)
        {
            StopCoroutine(lerpingFOV);
        }

        lerpingFOV = StartCoroutine(LerpFOVCoroutine(fov));
    }



    private void MoveCameraArm()
    {
        cameraArmPos = target.transform.position + nonCombatCameraArmPivotPos;

        cameraArm.transform.position = cameraArmPos;
    }

    private void MoveCamera()
    {
        Vector3 cameraPos = cameraArmPos + new Vector3(0, 0, -nonCombatCameraDist);

        transform.position = cameraPos;
    }

    private void RotateCameraArm()
    {
        cameraArm.transform.Rotate(Vector3.up, cameraLookingDelta.x * 0.2f, Space.World);

        cameraArm.transform.Rotate(Vector3.left, cameraLookingDelta.y * 0.15f, Space.World);

        // clamp x rotation to limit camera go up
        Vector3 cameraArmRotation = cameraArm.transform.rotation.eulerAngles;
        if (cameraArmRotation.x > 180f)
        {
            cameraArmRotation.x -= 360f;
        }
        float clampedCameraArmRotationX = Mathf.Clamp(cameraArmRotation.x, minRotationX, maxRotationX);
        Vector3 clampedCameraArmRotation = new Vector3(clampedCameraArmRotationX, cameraArmRotation.y, cameraArmRotation.z);
        cameraArm.transform.rotation = Quaternion.Euler(clampedCameraArmRotation);
    }

    private void RotateCamera()
    {
        transform.LookAt(cameraArm.transform);
    }

    private IEnumerator LerpPivotPosCoroutine(Vector3 newPivotPos)
    {
        yield return null;
    }

    private IEnumerator LerpFOVCoroutine(float fov)
    {
        while (Mathf.Approximately(fov, cam.fieldOfView) == false)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, Time.deltaTime * CameraFOVLerpInteria);
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

        // camera arm pivot
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(CameraArmPos, 0.1f);
        Handles.Label(CameraArmPos, "camera_arm_pivot_pos");

        // camera distance to camera arm pivot
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, cameraArmPos);
        Handles.Label((cameraArmPos + transform.position) * 0.5f, "camera_distance_to_pivot");
    }
#endif
}
