using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{

    public Rigidbody2D rb2d;
    private Vector2 movementVector;
    /*public float maxSpeed = 100;
    public float rotationSpeed = 70;
    public float acceleration = 100;
    public float deacceleration = 150;
    public float dragDeacceleration = 70;*/

    public TankMovementData movementData; //пресет параметров

    public float currentForwardDirection = 1;
    public float currentSpeed = 0;

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (movementVector.y > 0)
        {
            if (currentSpeed == 0)
            {
                currentSpeed += movementData.acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
            }
            else if (currentSpeed > 0)
            {
                currentSpeed += movementData.acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += movementData.deacceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -movementData.maxSpeed, 0);
            }
        }
        else if (movementVector.y == 0)
        {
            if (currentSpeed == 0) { }
            else if (currentSpeed > 0)
            {
                currentSpeed -= movementData.dragDeacceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += movementData.dragDeacceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -movementData.maxSpeed, 0);
            }
        }
        else if (movementVector.y < 0)
        {
            if (currentSpeed == 0)
            {
                currentSpeed -= movementData.acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -movementData.maxSpeed, 0);
            }
            else if (currentSpeed > 0)
            {
                currentSpeed -= movementData.deacceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
            }
            else if (currentSpeed < 0)
            {
                currentSpeed -= movementData.acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, -movementData.maxSpeed, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * currentSpeed * Time.fixedDeltaTime; //движение вперёд/назад
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime)); //поворот башни (проверить)
    }
}
