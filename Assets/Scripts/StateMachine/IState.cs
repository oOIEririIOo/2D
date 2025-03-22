

public abstract class IState
{
    protected FSM currentEnemy;
    public abstract void OnEnter(FSM enemy);

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void OnExit();
}
