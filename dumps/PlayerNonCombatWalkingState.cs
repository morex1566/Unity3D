using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNonCombatWalkingState : PlayerState
{
    private InputAction movingAction;

    private InputAction runningAction;

    private InputAction drawingAction;

    private InputAction targetingAction;



    public PlayerNonCombatWalkingState(PlayerBaseController baseController) : base(baseController)
    {
        movingAction = InputManager.InputMappingContext.Player.Move;
        runningAction = InputManager.InputMappingContext.Player.Run;
        drawingAction = InputManager.InputMappingContext.Player.Draw;
        targetingAction = InputManager.InputMappingContext.Player.Target;
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
        }

        /// TODO: must be later than <see cref="movingAction"/>
        if (runningAction.IsPressed() == true)
        {
            PlayerNonCombatRunningState runningState = new PlayerNonCombatRunningState(baseController);
            baseController.State = runningState;
            baseController.CameraController.LerpFOV(runningState.CamFOVAtRunningState);
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

        SetMovement(movingAction.ReadValue<Vector2>(), baseController.Status.LerpWeightIdleToWalking, baseController.Status.NonCombatWalkingSpeed);
        SetRotation(movingAction.ReadValue<Vector2>());
        SetAnimParameters();
    }

}
