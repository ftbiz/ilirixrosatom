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

        public void Init(Milestones milestones)
        {
            this.milestones = milestones;
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
            
        }
    }
}