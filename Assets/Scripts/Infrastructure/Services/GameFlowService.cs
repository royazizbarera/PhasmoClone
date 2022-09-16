using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowService : IService
{
    private GhostDataSO _currentGhost;
    private GhostDataSO _currentGhostChoosen;


    public void SetUpGameFlowService(GhostDataSO currentLevelGhost)
    {
        _currentGhost = currentLevelGhost;
    }

    public void ChangeCurrentChoosenGhost(GhostDataSO ghostChoosen)
    {
        _currentGhostChoosen = ghostChoosen;
    }

}
