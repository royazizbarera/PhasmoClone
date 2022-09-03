using UnityEngine;
using Utilities.Constants;
using GameFeatures;
namespace Ghosts.GhostMood
{
    public class GhostAnger : MonoBehaviour
    {
        [SerializeField]
        private GhostInfo _ghostInfo;
        [SerializeField]
        private GhostMood _ghostMood;

        [SerializeField]
        private RoomIdentifire _playerCurrentRoom;
        [SerializeField]
        private LevelRooms.LevelRoomsEnum _ghostRoom;

        private float _ghostAnger = 0f;
        private float _ghostAngerToAdd = 0f;
        private void Start()
        {
            _ghostAnger = _ghostInfo.GhostData.StartGhostAnger;
            _ghostMood.SetGhostAnger(_ghostAnger);

            _ghostRoom = _ghostInfo.GhostRoom;

            if (_ghostRoom == LevelRooms.LevelRoomsEnum.NoRoom) _ghostInfo.GhostSetedUp += SetUp;
        }

        public void AddGhostAngerWithCalc(float ghostAngerToAdd)
        {
            _ghostAngerToAdd = ghostAngerToAdd;

            _ghostAnger += _ghostAngerToAdd;
            _ghostMood.SetGhostAnger(_ghostAnger);
        }

        private void SetUp()
        {
            _ghostRoom = _ghostInfo.GhostRoom;
        }
    }
}