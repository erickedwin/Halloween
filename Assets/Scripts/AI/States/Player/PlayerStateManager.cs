using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.States;

public class PlayerStateManager : MonoBehaviour, IStateManager
{
    public IStateBase currentState { get; private set; }

    PlayerIdleState idle = new PlayerIdleState();

    PlayerMoveState move = new PlayerMoveState();

    public IStateBase GetState()
    {
        throw new System.NotImplementedException();
    }

    public void SwitchState(IStateBase state)
    {
        currentState = state;
    }

    private void Start()
    {
        SwitchState(idle);
        idle.EnterState(this);
    }

    private void Update()
    {
        
    }
}
