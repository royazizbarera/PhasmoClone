using GameFeatures;
using System;
using UnityEngine;
using Utilities.Constants;
public class GhostInfo : MonoBehaviour
{
    public GhostDataSO GhostData;
    public LevelRooms.LevelRoomsEnum GhostRoom;

    public Transform PlayerPoint;

    public RoomIdentifire PlayerRoom;

    public float FinalGhostAnger = 0f;

    public Action GhostSetedUp;
    public void SetUpGhost(Transform playerTransformPoint, LevelRooms.LevelRoomsEnum _ghostRoom, RoomIdentifire playerRoom)
    {
        PlayerPoint = playerTransformPoint;
        PlayerRoom = playerRoom;
        GhostRoom = _ghostRoom;
        GhostSetedUp?.Invoke();
    }
}
