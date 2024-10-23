using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/Player/Player Data")]
public class PlayerData : ScriptableObject
{
    public float CombatWalkingSpeed = 1.25f;
    public float NonCombatWalkingSpeed = 1.5f;
    public float RunningSpeed = 2.25f;
    public float InteriaFromIdleToWalking = 9f;
    public float InteriaFromWalkingToRunning = 3f;
}
