using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerSettings", menuName = "AstroEdit/SpawnerSettings")]
public class SpawnerSettings : ScriptableObject
{
    [Range(0f, 10f)] [SerializeField] public float _minSpawnTime, _maxSpawnTime;
    [Range(0f, 10f)] [SerializeField] public int _minAmount, _maxAmount;
}
