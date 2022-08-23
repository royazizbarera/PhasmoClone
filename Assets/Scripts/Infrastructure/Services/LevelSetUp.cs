using System;
using System.Collections;
using UnityEngine;
using Utilities.Constants;
namespace Infrastructure.Services
{
    public class LevelSetUp : IService
    {

        public Action OnLevelSetedUp;
        public LevelRooms.LevelRoomsEnum CurrGhostRoom
        {
            get { return _currRoom; }
        }         
        public Transform CurrGhostRoomTransform
        {
            get { return _currRoomTransform; }
        }

        private SceneNames.LevelNames _selectedMap;
        private Transform _currRoomTransform;
        private LevelRooms.LevelRoomsEnum _currRoom = LevelRooms.LevelRoomsEnum.NoRoom;
  
        private LevelInfo _currLevelInfo;

        public void ChooseMap(SceneNames.LevelNames selectedMap)
        {
            _selectedMap = selectedMap;
        }


        public Transform[] GetGhostPatrolPoints()
        {
            return _currLevelInfo.PatrolPoints;
        }

        public void InitializeLevel()
        {
            _currLevelInfo = GameObject.FindObjectOfType<LevelInfo>();
            if (_currLevelInfo == null)
            {
                Debug.LogWarning("Current level info = null!");
            }
            RandomizeCurrentRoom();
            OnLevelSetedUp?.Invoke();
        }

        public void ResetLevel()
        {
            _currRoom = LevelRooms.LevelRoomsEnum.NoRoom;
            _currRoomTransform = null;
        }

        private void RandomizeCurrentRoom()
        {
            int randomLevelNum = UnityEngine.Random.Range(0, _currLevelInfo.AllLevelRooms.Length);
            _currRoom = _currLevelInfo.AllLevelRooms[randomLevelNum].RoomType;
            _currRoomTransform = _currLevelInfo.AllLevelRooms[randomLevelNum].transform;

            //Debug.Log("curr room = " + _currRoom.ToString());
        }



    }
}