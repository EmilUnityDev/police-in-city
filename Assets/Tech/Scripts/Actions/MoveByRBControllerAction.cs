public class MoveByRBControllerAction : IAction
{
    private IMovementController _movementController;

    public MoveByRBControllerAction(IMovementController movementController)
    {
        _movementController = movementController;
    }

    public void Do()
    {
        _movementController.Move();
    }
}