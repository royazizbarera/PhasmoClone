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

        private LevelRooms.LevelRoomsEnum _nextRoom = LevelRooms.LevelRoomsEnum.NoRoom;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entered " + other.name);
            if (other.GetComponent<Room>() != null)
            {
                if (CurrRoom == LevelRooms.LevelRoomsEnum.NoRoom) CurrRoom = other.GetComponent<Room>().RoomType;
                else _nextRoom = other.GetComponent<Room>().RoomType;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Room>() != null)
            {
                if (CurrRoom == other.GetComponent<Room>().RoomType)
                {
                    if (_nextRoom != LevelRooms.LevelRoomsEnum.NoRoom && _nextRoom != CurrRoom)
                    {
                        CurrRoom = _nextRoom;
                    }
                    else
                    {
                        CurrRoom = LevelRooms.LevelRoomsEnum.NoRoom;
                    }
                }
                else if (_nextRoom == other.GetComponent<Room>().RoomType)
                {
                    _nextRoom = LevelRooms.LevelRoomsEnum.NoRoom;
                }
            }
        }
    }
}