using System.Collections;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace GraphVisual
{
    public class GraphController : MonoBehaviour
    {
        public string milestonesFileName = "Milestones.json";
        public GraphVisualizer graphVisualizer;

        private Milestone milestones;

        private bool fileDownloaded = false;
        private string fileText;

        private IEnumerator Start()
        {
            var url = Path.Combine(Application.streamingAssetsPath, milestonesFileName);

            Debug.Log("path = " + url);
            GetMilestonesFile(url);

            yield return new WaitUntil(() => fileDownloaded);

            ReadMilestonesData(fileText);

            graphVisualizer.Init(milestones);

            graphVisualizer.GenerateGraph();
        }

        private void GetMilestonesFile(string url)
        {
            
#if PLATFORM_WEBGL && !UNITY_EDITOR
            StartCoroutine(LoadStreamingAsset(url));
#else 
            if (File.Exists(url))
            {
                fileText = File.ReadAllText(url);
            }
            else
            {
                Debug.LogWarning("Can't find path with data.");
                return;
            }
            fileDownloaded = true;
#endif
            
        }

        private void ReadMilestonesData(string data)
        {
            Debug.Log("data = " + data);
            milestones = JsonUtility.FromJson<Milestone>(data);
            Debug.Log("milestones.milestones.Count = " + milestones);
        }

        IEnumerator LoadStreamingAsset(string url)
        {
            if (url.Contains("://") || url.Contains(":///"))
            {
                var www = UnityWebRequest.Get(url);
                yield return www.SendWebRequest();


                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    fileText = www.downloadHandler.text;
                    fileDownloaded = true;
                    // Show results as text
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }
}
