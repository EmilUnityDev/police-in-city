public class MoveByControllerAction : IAction
{
    private IMovementController _movementController;

    public MoveByControllerAction(IMovementController movementController)
    {
        _movementController = movementController;
    }

    public void Do()
    {
        _movementController.Move();
    }
}