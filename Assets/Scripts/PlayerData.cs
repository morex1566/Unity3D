using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/Player/Player DataPrefab")]
public class PlayerData : ScriptableObject
{
    public float CombatWalkingSpeed = 1.25f;
    public float NonCombatWalkingSpeed = 1.5f;
    public float RunningSpeed = 5f;
}
