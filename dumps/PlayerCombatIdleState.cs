using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatIdleState : PlayerState
{
    private InputAction movingAction;

    private InputAction drawingAction;

    private InputAction targetingAction;



    public PlayerCombatIdleState(PlayerBaseController baseController) : base(baseController)
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
            baseController.State = new PlayerCombatWalkingState(baseController);
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
    }

    protected override void SetRotation(Vector2 inputDirection)
    {

    }

    protected override void SetMovement(Vector2 inputDirection, float lerpWeight, float speed)
    {

    }
}
