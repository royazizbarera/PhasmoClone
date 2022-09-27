using GameFeatures;
using System;
using UnityEngine;
using Utilities.Constants;
public class GhostInfo : MonoBehaviour
{
    public GhostDataSO GhostData;
    public LevelRooms.LevelRoomsEnum GhostRoom;
    public LevelSizeConst.LevelSize LevelSize;

    public Transform PlayerPoint;
    public Transform PlayerTransform;
    public DoorDraggable[] MainDoors;
    public SanityHandler PlayerSanity;
    public RoomIdentifire PlayerRoom;
    public bool SetedUp = false;
    public float FinalGhostAnger = 0f;

    public Action GhostSetedUp;
    public void SetUpGhost(GhostDataSO currGhostData, Transform playerTransform, Transform playerTransformPoint, LevelRooms.LevelRoomsEnum _ghostRoom, RoomIdentifire playerRoom, SanityHandler playerSanity, LevelSizeConst.LevelSize levelSize, DoorDraggable[] mainDoors)
    {
        GhostData = currGhostData;

        PlayerTransform = playerTransform;
        PlayerPoint = playerTransformPoint;

        PlayerRoom = playerRoom;
        PlayerSanity = playerSanity;

        GhostRoom = _ghostRoom;
        LevelSize = levelSize;

        MainDoors = mainDoors;

        GhostSetedUp?.Invoke();
        SetedUp = true;
    }
}
