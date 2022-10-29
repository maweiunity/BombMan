using UnityEngine;

[CreateAssetMenu(fileName = "GamePrefabs", menuName = "BombMan/GamePrefabs", order = 0)]
public class GamePrefabs : ScriptableObject
{
    [Header("武器")]
    [Tooltip("炸弹")]
    public GameObject Bomb;
}