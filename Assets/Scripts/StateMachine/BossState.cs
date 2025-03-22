

public abstract class BossState
{
    protected BossFSM currentEnemy;
    public abstract void OnEnter(BossFSM enemy);

    public abstract void LogicUpdate(BossFSM enemy);

    public abstract void PhysicsUpdate();

    public abstract void OnExit();
}
