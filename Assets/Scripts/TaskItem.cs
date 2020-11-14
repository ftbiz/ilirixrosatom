using UnityEngine;

namespace GraphVisual
{
    public class TaskItem : MonoBehaviour
    {
        public string id;

        public Vector3 startPoint;
        public Vector3 endPoint;

        public void Init(MilestoneTask milestoneTask, Vector3 startPoint, Vector3 endPoint)
        {
            id = milestoneTask.id;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            
            Debug.Log("startPoint = " + startPoint);
            Debug.Log("endPoint = " + endPoint);
            var centerPosX = startPoint.x + Mathf.Abs(endPoint.x - startPoint.x) / 2;
            Debug.Log("centerPosX = " + centerPosX);
            transform.position = startPoint;
        }
    }
}