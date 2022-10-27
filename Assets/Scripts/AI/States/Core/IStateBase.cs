namespace Enemy.States
{
    //The interface that all states will use to share the global methods.
    //Example: if you are adding the state of a Enemy (like Move, idle, attack), that state must inherent from this interface to be detected later.
    public interface IStateBase
    {
        public void EnterState(IStateManager stateManager);

        public void ExitState(IStateManager stateManager);

        public void UpdateState(IStateManager stateManager);

        public void SetState(IStateManager stateManager);
    }
}