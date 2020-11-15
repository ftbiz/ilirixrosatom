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

        private Milestone milestones;
        private List<TaskItem> allTasks;

        private Axis currentAxis = Axis.y;
        private int currentSign = 1;
        
        private enum Axis
        {
            y,
            z
        }

        public void Init(Milestone milestones)
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

            var chain = milestones.chain;

            if (chain != null)
            {
                GenerateChain(chain);
            }
        }

        private void GenerateChain(List<MilestoneTask> milestoneTasks)
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

                    if (!string.IsNullOrEmpty(milestoneTask.parentId))
                    {
                        var parentTask = allTasks.Find(x => x.ID == milestoneTask.parentId);

                        if (parentTask != null)
                        {
                            if (parentTask.IsMultiParent)
                            {
                                if (parentTask.IsFirstChildSet)
                                {
                                    var addValue = new Vector3();
                                    if (currentAxis == Axis.y)
                                    {
                                        addValue = new Vector3(0, distanceBetweenChains * currentSign, 0);
                                        currentAxis = Axis.z;
                                    }
                                    else
                                    {
                                        addValue = new Vector3(0, 0, distanceBetweenChains * currentSign);
                                        currentAxis = Axis.y;
                                    }

                                    currentSign *= -1;

                                    startPointPosition += addValue;
                                    startPoint = Instantiate(pointPrefab, startPointPosition, Quaternion.identity);
                                    CreateLine(parentTask.EndPoint, startPointPosition);
                                    distanceBetweenChains++;
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