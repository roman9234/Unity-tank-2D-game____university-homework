using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTankMovementData", menuName = "Data/TankMovementData")]
public class TankMovementData : ScriptableObject //позволяет задавать параметры
{
    public float maxSpeed = 100;
    public float rotationSpeed = 70;
    public float acceleration = 150;
    public float deacceleration = 200;
    public float dragDeacceleration = 70;
}
