using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.States;
using Pathfinding;

public class EnemyStateManager : MonoBehaviour, IStateManager
{

    public IStateBase currentState { get; private set; }

    public EnemyIdleState idle = new EnemyIdleState();

    public EnemyPatrolState patrol = new EnemyPatrolState();

    public EnemyTargetingState targeting = new EnemyTargetingState();

    public EnemySearchingState searching = new EnemySearchingState();

    public EnemyAttackingState attacking = new EnemyAttackingState();

    public EnemyMovingState moving = new EnemyMovingState();


    // TODO: enlazarlo con un radar.
    void Start()
    {
        SwitchState(idle);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(IStateBase state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    
    public void DetectPlayer()
    {
        SwitchState(targeting);
    }

    public void MoveToSwitch()
    {

    }

    public IStateBase GetState() => currentState;
}
