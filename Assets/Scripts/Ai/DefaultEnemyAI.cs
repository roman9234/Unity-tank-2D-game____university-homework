using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour //finite state machine для более продвинутого ИИ
{
    // Start is called before the first frame update
    [SerializeField]
    private AIBehaviour shootBehaviour, patrolBehaviour;

    [SerializeField]
    TankController tank;
    [SerializeField]
    private AIDetector detector;

    private void Awake()
    {
        detector  = GetComponentInChildren<AIDetector>();
        tank  = GetComponentInChildren<TankController>();
    }

    private void Update()
    {
        if (detector.TargetVisible) 
        {
            shootBehaviour.PerformAction(tank, detector);
        }
        else
        {
            patrolBehaviour.PerformAction(tank, detector);
        }
    }

}
