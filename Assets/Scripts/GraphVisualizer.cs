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

        public GameObject LinePrefab;

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
            var needLine = false;
            
            foreach (var milestoneTask in milestoneTasks)
            {
                if (!allTasks.Exists(x => x.ID == milestoneTask.id))
                {
                    var taskObject = Instantiate(taskPrefab);
                    var taskItem = taskObject.GetComponent<TaskItem>();

                    var startPointPosition = startPoint.transform.position;

                    if (milestoneTask.parentId != null)
                    {
                        var parentTask = allTasks.Find(x => x.ID == milestoneTask.parentId);

                        if (parentTask != null)
                        {
                            if (parentTask.IsMultiParent)
                            {
                                if (parentTask.IsFirstChildSet)
                                {
                                    startPointPosition += new Vector3(0, distanceBetweenChains, 0);
                                    startPoint = Instantiate(pointPrefab, startPointPosition, Quaternion.identity);
                                    CreateLine(parentTask.EndPoint, startPointPosition);
                                }
                                else
                                {
                                    parentTask.IsFirstChildSet = true;
                                }
                            }
                        }
                    }
                    
                    var endPoint = Instantiate(pointPrefab, new Vector3(startPointPosition.x + defaultTaskDuration,
                        startPointPosition.y, startPointPosition.z), Quaternion.identity);
                    taskItem.Init(milestoneTask, startPointPosition, endPoint.transform.position);

                    startPoint = endPoint;
                    
                    allTasks.Add(taskItem); 
                }
            }
        }

        private void CreateLine(Vector3 startPosition, Vector3 endPosition)
        {
            var line = Instantiate(LinePrefab);
            var lineRenderer = line.GetComponent<LineRenderer>();
            
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
        }
    }
}