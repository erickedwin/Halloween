using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates
{
    //I'm leaving it blanck for now
}

namespace Enemy.States
{
    public class PlayerIdleState : IStateBase
    {
        public void EnterState(IStateManager stateManager)
        {
            Debug.Log("Entered Idle State");
        }

        public void ExitState(IStateManager stateManager)
        {
            throw new System.NotImplementedException();
        }

        public void SetState(IStateManager stateManager)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState(IStateManager stateManager)
        {
            Debug.Log("Updating Idle State");
        }
    }

    public class PlayerMoveState : IStateBase
    {
        public void EnterState(IStateManager stateManager)
        {
            Debug.Log("Entered Move State");
        }

        public void ExitState(IStateManager stateManager)
        {
            throw new System.NotImplementedException();
        }

        public void SetState(IStateManager stateManager)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState(IStateManager stateManager)
        {
            Debug.Log("Updating Move State");
        }
    }
}

