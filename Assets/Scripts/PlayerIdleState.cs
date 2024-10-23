using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{
    private InputAction movingAction;

    private InputAction drawingAction;

    private InputAction targetingAction;



    public PlayerIdleState(PlayerCharacterController controller) : base(controller)
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
            controller.State = new PlayerWalkingState(controller);
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
        controller.MoveDistance = Mathf.Abs(Mathf.Lerp(controller.MoveDistance, 0f, Time.deltaTime * controller.Status.InteriaFromIdleToWalking));
        controller.MoveDirection = Vector3.Slerp(controller.MoveDirection, inputDirection3D, Time.deltaTime * controller.Status.InteriaFromIdleToWalking);
    }
}
