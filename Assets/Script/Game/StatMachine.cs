using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine :MonoBehaviour
{
    protected State State;
    public void SetState(State state) 
    {
        State = state;
        StartCoroutine(State.Jump());
        StartCoroutine(State.Move());
        StartCoroutine(State.Idle());
    }
}
