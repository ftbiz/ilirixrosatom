using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace GraphVisual
{
    public class GraphController : MonoBehaviour
    {
        public string milestonesFileName = "Milestones.json";
        public GraphVisualizer graphVisualizer;

        private Milestones milestones;

        private void Awake()
        {
            var path = Path.Combine(Application.streamingAssetsPath, milestonesFileName);
            Debug.Log("path = " + path);
            milestones = ReadMilestonesData(path);

            graphVisualizer.Init(milestones);
        }

        private void Start()
        {
            graphVisualizer.GenerateGraph();
        }

        private Milestones ReadMilestonesData(string path)
        {
            if (File.Exists(path))
            {
                var data = File.ReadAllText(path);
                var milestones = JsonConvert.DeserializeObject<Milestones>(data);
                return milestones;
            }

            Debug.LogWarning("Can't find path with data.");
            return null;
        }
    }
}
