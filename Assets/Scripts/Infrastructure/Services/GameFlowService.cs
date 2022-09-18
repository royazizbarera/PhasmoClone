using Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameFlowService : IService
    {
        public Action GameOverAction;
        public Action WinAction;

        public GhostDataSO GhostDataSO
        {
            get { return _currentGhostSO; }
        }

        private GhostInfo _currentGhostInfo;
        private GhostDataSO _currentGhostSO;
        private GhostDataSO _currentGhostChoosenSO;

        public void SetUpGameFlowService(GhostInfo currentLevelGhost)
        {
            _currentGhostInfo = currentLevelGhost;
            _currentGhostSO = _currentGhostInfo.GhostData;
        }

        public void ChangeCurrentChoosenGhost(GhostDataSO ghostChoosen)
        {
            _currentGhostChoosenSO = ghostChoosen;
        }

    }
}