using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNonCombatRunningState : PlayerState
{
    private InputAction movingAction;

    private InputAction runningAction;

    private InputAction drawingAction;

    private InputAction targetingAction;

    // during runningAction state
    private float cameraFOVAtRunningState;
    public float CamFOVAtRunningState
    {
        get { return cameraFOVAtRunningState; }
        set { cameraFOVAtRunningState = value; }
    }



    public PlayerNonCombatRunningState(PlayerBaseController controller) : base(controller)
    {
        movingAction = InputManager.InputMappingContext.Player.Move;
        runningAction = InputManager.InputMappingContext.Player.Run;
        drawingAction = InputManager.InputMappingContext.Player.Draw;
        targetingAction = InputManager.InputMappingContext.Player.Target;

        cameraFOVAtRunningState = controller.CameraController.CamFOV + 8f;
    }

    public override void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public override void Update()
    {
        if (movingAction.IsPressed() == false)
        {
            baseController.State = new PlayerNonCombatIdleState(baseController);
            baseController.CameraController.LerpFOV(baseController.CameraController.CamFOV);
        }

        /// TODO: must be later than <see cref="movingAction"/>
        if (runningAction.IsPressed() == false)
        {
            baseController.State = new PlayerNonCombatWalkingState(baseController);
            baseController.CameraController.LerpFOV(baseController.CameraController.CamFOV);
        }

        if (drawingAction.triggered == true)
        {
            baseController.IsWeaponDrawn = baseController.IsWeaponDrawn ? false : true;
            baseController.OnWeaponDraw = true;
        }

        if (targetingAction.triggered == true)
        {
            baseController.IsInCombat = baseController.IsInCombat ? false : true;
        }

        SetMovement(movingAction.ReadValue<Vector2>(), baseController.Status.LerpWeightWalkingToRunning, baseController.Status.RunningSpeed);
        SetRotation(movingAction.ReadValue<Vector2>());
        SetAnimParameters();
    }
}
