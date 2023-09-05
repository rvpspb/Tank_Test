using UnityEngine;
using tank.factory;
using tank.config;
using tank.input;

namespace tank.core
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerStartPoint { get; private set; }
        
        public void Construct()
        {
            
        }

        public Vector3 GetStartPosition(PaddleSide paddleSide)
        {
            return PlayerStartPoint.position;
        }
    }
}
