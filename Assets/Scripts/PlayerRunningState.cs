using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerState
{
    private InputAction movingAction;

    private InputAction runningAction;

    private InputAction drawingAction;

    private InputAction targetingAction;

    // during runningAction state
    private float cameraFOVAtRunningState;
    public float CameraFOVAtRunningState
    {
        get { return cameraFOVAtRunningState; }
        set { cameraFOVAtRunningState = value; }
    }



    public PlayerRunningState(PlayerCharacterController controller) : base(controller)
    {
        movingAction = InputManager.InputMappingContext.Player.Move;
        runningAction = InputManager.InputMappingContext.Player.Run;
        drawingAction = InputManager.InputMappingContext.Player.Draw;
        targetingAction = InputManager.InputMappingContext.Player.Target;

        cameraFOVAtRunningState = controller.CameraController.CameraFOV + 8f;
    }

    public override void FixedUpdate()
    {
        Move();
        Rotate();
        Rotate(controller.CameraController.transform.forward);
    }

    public override void Update()
    {
        if (movingAction.IsPressed() == false)
        {
            controller.State = new PlayerIdleState(controller);
            controller.CameraController.LerpFOV(controller.CameraController.CameraFOV);
        }

        /// TODO: must be later than <see cref="movingAction"/>
        if (runningAction.IsPressed() == false)
        {
            controller.State = new PlayerWalkingState(controller);
            controller.CameraController.LerpFOV(controller.CameraController.CameraFOV);
        }

        if (drawingAction.triggered == true)
        {
            controller.IsWeaponDrawn = controller.IsWeaponDrawn ? false : true;
            controller.OnWeaponDraw = true;
        }

        if (targetingAction.triggered == true)
        {
            controller.IsInCombat = controller.IsInCombat ? false : true;
        }

        SetMovement();
        SetAnimParameters();
    }

    protected override void SetMovement()
    {
        Vector2 inputDirection2D = movingAction.ReadValue<Vector2>();
        Vector3 inputDirection3D = new Vector3(inputDirection2D.x, 0f, inputDirection2D.y);

        controller.InputDirection = inputDirection2D;
        controller.MoveDistance = Mathf.Abs(Mathf.Lerp(controller.MoveDistance, controller.Data.RunningSpeed, Time.deltaTime * controller.Status.InteriaFromWalkingToRunning));
        controller.MoveDirection = Vector3.Slerp(controller.MoveDirection, inputDirection3D, Time.deltaTime * controller.Status.InteriaFromWalkingToRunning);
    }
}
