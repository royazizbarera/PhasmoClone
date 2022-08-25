using UnityEngine;

[CreateAssetMenu(fileName = "Ghost", menuName = "ScriptableObjects/GhostData", order = 1)]
public class GhostDataSO : ScriptableObject
{
    public float GhostNormalSpeed = 2;
    public float GhostAttackSpeed = 2;

    [Tooltip("The lower this number,the more time ghost will spend in his room")]
    public int PatrolRandomMultiplier = 5;

    public float ItemThrowRadiusOfInterection = 1.5f;
    public float DoorsTouchRadiusInterection = 2.5f;
    public float ClickableTouchRadiusInterection = 2.5f;

    public float ItemThrowPower = 5f;

    public float MinMeshActiveTime = 0.12f;
    public float MaxMeshActiveTime = 0.18f;

    public float MinMeshDisabledTime = 0.55f;
    public float MaxMeshDisabledTime = 0.9f;

    public float MaxDistanceToPlayerAggre = 4f;
}