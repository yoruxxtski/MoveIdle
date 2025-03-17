using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public StateControllerBase stateController;
    public virtual void OnEnter(StateControllerBase stateController) {
        this.stateController = stateController;
    }
    public virtual void OnUpdate() {}
    public virtual void OnFixedUpdate() {}
    public virtual void OnExit() {
        if (stateController is PlayerStateMachine) {
            PlayerStateMachine playerStateMachine = stateController as PlayerStateMachine;
            playerStateMachine.playerAnimation.ResetAnimation();
        }
        if (stateController is EnemyStateMachine) {
            EnemyStateMachine enemyStateMachine = stateController as EnemyStateMachine;
            enemyStateMachine.enemyAnimation.ResetAnimation();
        }
    }
}
