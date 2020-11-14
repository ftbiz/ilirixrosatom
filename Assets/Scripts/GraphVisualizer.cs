using System.Collections.Generic;
using UnityEngine;

namespace GraphVisual
{
    public class GraphVisualizer : MonoBehaviour
    {
        public Vector3 defaultStartPosition;
        public float distanceBetweenChains;

        public int defaultTaskDuration;

        public GameObject pointPrefab;
        public GameObject taskPrefab;

        private Milestones milestones;
        private List<TaskItem> allTasks;

        public void Init(Milestones milestones)
        {
            this.milestones = milestones;
            allTasks = new List<TaskItem>();
        }

        public void GenerateGraph()
        {
            if (milestones == null)
            {
                Debug.LogWarning("Milestones are empty.");
                return;
            }
            
            foreach (var milestone in milestones.milestones)
            {
                var chain = milestone.chain;

                if (chain != null)
                {
                    GenerateChain(chain);
                }
            }
        }

        private void GenerateChain(MilestoneTask[] milestoneTasks)
        {
            if (milestoneTasks == null)
            {
                Debug.LogWarning("No tasks in chain!");
                return;
            }

            var startPoint = Instantiate(pointPrefab, defaultStartPosition, Quaternion.identity);
            
            foreach (var milestoneTask in milestoneTasks)
            {
                if (!allTasks.Exists(x => x.id == milestoneTask.id))
                {
                    var taskObject = Instantiate(taskPrefab);
                    var taskItem = taskObject.GetComponent<TaskItem>();
                    
                    var startPointPosition = startPoint.transform.position;

                    var endPoint = Instantiate(pointPrefab, new Vector3(startPointPosition.x + defaultTaskDuration,
                        startPointPosition.y, startPointPosition.z), Quaternion.identity);
                    taskItem.Init(milestoneTask, startPointPosition, endPoint.transform.position);

                    startPoint = endPoint;
                    
                    allTasks.Add(taskItem); 
                }
            }
        }
    }
}