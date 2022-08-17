using UnityEngine;
using Infrastructure.States.GameStates;
using Infrastructure;
using Utilities.Constants;
using Environment;
using Infrastructure.Services;

namespace UI
{
    public class BoardMenu : MonoBehaviour
    {
        private BoardClickable _board;

        private void Start()
        {
            _board = GetComponent<BoardClickable>();
        }
        public void LoadLevel()
        {
            _board.UnlockInputControl();
            GameBootstrapper.Instance.StateMachine.Enter<LoadLevelState>();
        }
    }
}