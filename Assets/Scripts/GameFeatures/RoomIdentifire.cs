using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Constants;

namespace GameFeatures
{
    [RequireComponent(typeof(Collider))]
    public class RoomIdentifire : MonoBehaviour
    {
        public LevelRooms.LevelRoomsEnum CurrRoom = LevelRooms.LevelRoomsEnum.NoRoom;

        public List<LevelRooms.LevelRoomsEnum> _nextRooms = new List<LevelRooms.LevelRoomsEnum>();

        private void OnTriggerEnter(Collider other)
        {
            //   Debug.Log("Entered " + other.name);
            if (other.GetComponent<Room>() != null)
            {
                if (CurrRoom == LevelRooms.LevelRoomsEnum.NoRoom) CurrRoom = other.GetComponent<Room>().RoomType;
                else _nextRooms.Add(other.GetComponent<Room>().RoomType);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Room>() != null)
            {
                if (CurrRoom == other.GetComponent<Room>().RoomType)
                {
                    if (_nextRooms.Count > 0)// && _nextRoom != CurrRoom)
                    {
                        CurrRoom = _nextRooms[0];
                        _nextRooms.Remove(CurrRoom);
                    }
                    else
                    {
                        CurrRoom = LevelRooms.LevelRoomsEnum.NoRoom;
                    }
                }
                else if (_nextRooms.Contains(other.GetComponent<Room>().RoomType))
                {
                    _nextRooms.Remove(other.GetComponent<Room>().RoomType);
                }
            }
        }
    }
}