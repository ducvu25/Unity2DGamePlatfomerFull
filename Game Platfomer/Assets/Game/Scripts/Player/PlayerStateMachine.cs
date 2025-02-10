using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState currentState { get; private set; }
    public PlayerStateMachine() { }
    // Start is called before the first frame update
    public void Initalize(PlayerState state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void ChangeState(PlayerState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
