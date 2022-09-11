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

        public LevelSizeConst.LevelSize CurrLevelSize
        {
            get { return _currLevelSize; }
        }
        public Transform CurrGhostRoomTransform
        {
            get { return _currRoomTransform; }
        }
        public int[] AddedItems
        {
            get { return _addedItems; }
        }

        public GameObject MainPlayer;
        public GhostInfo GhostInfo;

        private SceneNames.LevelNames _selectedMap;
        private Transform _currRoomTransform;
        private LevelRooms.LevelRoomsEnum _currRoom = LevelRooms.LevelRoomsEnum.NoRoom;
  
        private LevelInfo _currLevelInfo;
        private LevelSizeConst.LevelSize _currLevelSize;

        private int[] _addedItems;

        public void ChooseMap(SceneNames.LevelNames selectedMap)
        {
            _selectedMap = selectedMap;
        }
        public void SetAddedItems(int[] addedItems)
        {
            _addedItems = addedItems;
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
            _currLevelSize = _currLevelInfo.LevelSize;
            _currRoomTransform = _currLevelInfo.AllLevelRooms[randomLevelNum].transform;

            //Debug.Log("curr room = " + _currRoom.ToString());
        }
    }
}