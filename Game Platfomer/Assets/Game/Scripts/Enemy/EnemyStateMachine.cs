using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState state { get; private set; }
    public EnemyStateMachine() { }

    public void InitState(EnemyState state)
    {
        this.state = state;
        state.Enter();
    }

    public void SetState(EnemyState state)
    {
        this.state.Exit();
        this.state = state;
        this.state.Enter();
    }
}
