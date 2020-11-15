using UnityEngine;
using TMPro;

namespace GraphVisual
{
    public class TaskItem : MonoBehaviour
    {
        public TMP_Text taskName;
        
        public string ID { get; private set; }
        public bool IsMultiParent { get; private set; }
        public bool IsFirstChildSet { get; set; }

        private Vector3 StartPoint { get; set; }
        public Vector3 EndPoint { get; set; }

        public void Init(MilestoneTask milestoneTask, Vector3 startPoint, Vector3 endPoint)
        {
            ID = milestoneTask.id;
            IsMultiParent = milestoneTask.isMultiParent;
            IsFirstChildSet = false;

            taskName.text = milestoneTask.id;
            
            StartPoint = startPoint;
            EndPoint = endPoint;
            
            Debug.Log("startPoint = " + startPoint);
            Debug.Log("endPoint = " + endPoint);
            var centerPosX = startPoint.x + Mathf.Abs(endPoint.x - startPoint.x) / 2;
            Debug.Log("centerPosX = " + centerPosX);
            transform.position = startPoint;
        }
    }
}