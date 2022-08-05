using UnityEngine;

public class BootState : IState
{

    public void Enter()
    {
        Debug.Log("entered");
    }

    public void Exit()
    {
    }
}