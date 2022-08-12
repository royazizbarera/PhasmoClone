using Infrastructure;
using Infrastructure.States;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    public GameStateMachine StateMachine;

    private void Awake() //boot
    {
        StateMachine = new GameStateMachine(new AllServices(), this);
        StateMachine.Enter<BootState>();

        DontDestroyOnLoad(this);
    }
}
