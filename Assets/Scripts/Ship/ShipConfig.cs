using UnityEngine;
using Variables;

namespace Ship
{
    [CreateAssetMenu(fileName = "Ship Config", menuName = "ScriptableObjects/ShipConfig", order = 0)]
    public class ShipConfig : ScriptableObject
    {
        [Range(0f, 10f)] [SerializeField] public float throttlePower;
        [Range(0f, 10f)] [SerializeField] public float rotationPower;
        [Range(0f, 0.5f)] [SerializeField] public float laserSpeed;
    }
    
}
