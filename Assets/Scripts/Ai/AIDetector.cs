using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField]
    private float viewRadius = 11;
    [SerializeField]
    private float detectionCheckDelay = 0.1f; //чтобы преверка не происходила каждый кадр
    [SerializeField]
    private Transform target = null; // наёденная цель
    [SerializeField]
    private LayerMask playerLayerMask; //уровень на котором должен быть объект чтобы быть отмеченным как цель
    [SerializeField]
    private LayerMask visibilityLayer; // уровень сокрытия видимости

    [field: SerializeField] //показывает текущую цель

    public bool TargetVisible { get; private set; }

    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Update()
    {
        if (Target != null)
            TargetVisible = CheckTargetVisibile();
    }

    private bool CheckTargetVisibile()
    {
        // рэйкаст если есть цель
        var result = Physics2D.Raycast(transform.position, Target.position - transform.position, viewRadius, visibilityLayer);
        if (result.collider != null) 
        {
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0; //битовые операции с масками уровней (скопировал)
        }
        return false;
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutline());
    }

    private void DetectTarget() //проверка есть ли цель /появилась/исчезла
    {
        if (target == null)
            CheckIfPlayerInRange();
        else if (target != null)
            DetectIfOutRange();
    }


    /*
     Если игрок неактивен (мёртв), не задан, или дальше радиуса обзора, цели не будет
     */
    private void DetectIfOutRange() 
    {
        if (Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(transform.position, Target.position) > viewRadius)
        {
            Target = null;
            //TargetVisible = false; //ред
        }
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask); //проверяет есть ли коллайдер в круглой зоне вокруг детектора
        if (collision != null)
        {
            Target = collision.transform;
            //TargetVisible = true; //ред
        }
    }
    IEnumerator DetectionCoroutline() //цикл с задержкой в 0.1 секунду
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutline());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius); //рисует дальность видимости
    }

}
