using System.Collections;
using UnityEngine;
using Utilities.Constants;
namespace Infrastructure.Services
{
    public class LevelSetUp : IService
    {
        public LevelRooms.LevelRoomsEnum CurrRoom
        {
            get { return _currRoom; }
        }         
        public Transform CurrRoomTransform
        {
            get { return _currRoomTransform; }
        }

        private SceneNames.LevelNames _selectedMap;

        private Transform _currRoomTransform;
        private LevelRooms.LevelRoomsEnum _currRoom;

       
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
        }

        private void RandomizeCurrentRoom()
        {
            int randomLevelNum = Random.Range(0, _currLevelInfo.AllLevelRooms.Length);

            _currRoom = _currLevelInfo.AllLevelRooms[randomLevelNum].RoomType;
            _currRoomTransform = _currLevelInfo.AllLevelRooms[randomLevelNum].transform;

            //Debug.Log("curr room = " + _currRoom.ToString());
        }



    }
}