using UnityEngine;

namespace tank.config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public PaddleType Player1Type { get; private set; }
        [field: SerializeField] public PaddleType Player2Type { get; private set; }
        [field: SerializeField] public int WinScore { get; private set; }
        [field: SerializeField] public float GamePeriod { get; private set; }
        [field: SerializeField] public float PuddleSpeed { get; private set; }
        [field: SerializeField] public float PuddleSize { get; private set; }
    }
}

public enum PaddleType
{
    Player,
    Bot
}
