using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject //позволяет задавать параметры
{
    public float speed = 100;
    public int damage = 10;
    public float maxDistance = 100;
}
