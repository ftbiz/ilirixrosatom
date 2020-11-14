using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace GraphVisual
{
    public class GraphVisualizer : MonoBehaviour
    {
        public string milestonesFileName = "Milestones.json";

        private void Awake()
        {
            var path = Path.Combine(Application.streamingAssetsPath, milestonesFileName);
            Debug.Log("path = " + path);
            var milestones = ReadMilestonesData(path);
            Debug.Log("milestones.milestones.Length = " + milestones.milestones.Length);
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
