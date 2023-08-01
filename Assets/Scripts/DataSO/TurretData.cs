using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "Data/TurretData")]
public class TurretData : ScriptableObject //позволяет задавать параметры
{
    public float reloadDelay = 1;
    public GameObject bulletPrefab;
    public BulletData bulletData;
}
