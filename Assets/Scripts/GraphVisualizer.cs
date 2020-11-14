using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GraphVisual
{
    public class GraphVisualizer : MonoBehaviour
    {
        public string path = "";

        private void Awake()
        {
            var milestones = ReadMilestonesData();
        }

        private List<Milestone> ReadMilestonesData()
        {
            if (File.Exists(path))
            {
                var data = File.ReadAllText(path);
                var milestones = JsonUtility.FromJson<List<Milestone>>(data);
                return milestones;
            }

            Debug.LogWarning("Can't find path with data.");
            return null;
        }
    }
}
