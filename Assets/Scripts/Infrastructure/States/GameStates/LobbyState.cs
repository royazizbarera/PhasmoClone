using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.States.GameStates
{
    public class LobbyState : IState
    {
        public void Enter()
        {
            Debug.Log("Lobby started");
        }

        public void Exit()
        {
        }
    }
}