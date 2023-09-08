using UnityEngine;

namespace tank.config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {        
        [field: SerializeField] public int BotsCount { get; private set; }        
    }
}