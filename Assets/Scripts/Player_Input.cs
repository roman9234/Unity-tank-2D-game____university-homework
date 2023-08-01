using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Input : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();

    private void Awake() {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        GetBodyMovement();
        GetTurretMovement();
        GetShootingInput();
    }

    private void GetShootingInput()
    {
        // throw new NotImplementedException();
        if(Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
        }
    }

    private void GetTurretMovement()
    {
        // throw new NotImplementedException();
        OnMoveTurret?.Invoke(GetMousePosition());
    }

    private Vector2 GetMousePosition() //возвращает вектор 2 который идёт в событие
    {
        Vector3 mousePosition = Input.mousePosition; //вектор 3 - позиция мышки
        mousePosition.z = mainCamera.nearClipPlane; // z ось изменяем
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition); //
        return mouseWorldPosition;
    }

    private void GetBodyMovement()
    {
        // throw new NotImplementedException();
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);
    }


}
