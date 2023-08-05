using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();

    public int Length {  get { return patrolPoints.Count; } }

    [Header("Gizmos parameters")] //отображение при разработке
    public Color pointsColor = Color.blue;
    public float pointSize = 0.3f;
    public Color lineColor = Color.magenta;


    public struct PathPoint //будет отправляться в скрипт с ИИ
    {
        public int Index;
        public Vector2 Position;
    }

    public PathPoint GetClosestPathPoint(Vector2 tankPosition) //поиск ближайше точки маршрута для ИИ
    {
        var minDistance = float.MaxValue;
        var index = -1;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            var tempDistance = Vector2.Distance(tankPosition, patrolPoints[i].position);
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                index = i;
            }
        }

        return new PathPoint { Index = index, Position = patrolPoints[index].position };
    }

    public PathPoint GetNextPathPoint(int index)
    {
        var newIndex = index + 1 >= patrolPoints.Count ? 0 : index + 1; // не выходим за пределы списка точек
        return new PathPoint { Index = newIndex, Position = patrolPoints[newIndex].position };
    }

    private void OnDrawGizmos() //отображение в редакторе
    {
        if (patrolPoints.Count == 0)
            return;
        for (int i = patrolPoints.Count - 1; i >= 0; i--)
        {
            if (i == -1 || patrolPoints[i] == null)
                return;

            Gizmos.color = pointsColor;
            Gizmos.DrawSphere(patrolPoints[i].position, pointSize);

            if (patrolPoints.Count == 1 || i == 0)
                return;

            Gizmos.color = lineColor;
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i-1].position);

            if (patrolPoints.Count > 2 && i == patrolPoints.Count - 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
            }
        }
    }








}
