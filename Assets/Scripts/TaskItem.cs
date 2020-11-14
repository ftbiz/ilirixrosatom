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
        }
    }
}