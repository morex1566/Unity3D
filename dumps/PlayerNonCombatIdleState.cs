using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNonCombatIdleState : PlayerState
{
    private InputAction movingAction;

    private InputAction drawingAction;

    private InputAction targetingAction;



    public PlayerNonCombatIdleState(PlayerBaseController baseController) : base(baseController)
    {
        movingAction = InputManager.InputMappingContext.Player.Move;
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
        if (movingAction.inProgress == true)
        {
            baseController.State = new PlayerNonCombatWalkingState(baseController);
        }

        if (drawingAction.triggered == true)
        {
            baseController.IsWeaponDrawn = baseController.IsWeaponDrawn ? false : true;
            baseController.OnWeaponDraw = true;
        }

        if (targetingAction.triggered == true)
        {
            baseController.IsInCombat = baseController.IsInCombat ? false : true;
            baseController.State = new PlayerCombatIdleState(baseController);
        }

        SetMovement(movingAction.ReadValue<Vector2>(), baseController.Status.LerpWeightIdleToWalking, 0f);
        SetRotation(movingAction.ReadValue<Vector2>());
        SetAnimParameters();
    }
}
