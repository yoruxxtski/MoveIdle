using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateControllerBase : MonoBehaviour
{
    public StateBase currentState;
    void Update()
    {
        currentState?.OnUpdate();
    }
    void FixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }
    public void ChangeState(StateBase newState) {
        currentState?.OnExit();
        currentState = newState;
        newState.OnEnter(this);
    }
}
