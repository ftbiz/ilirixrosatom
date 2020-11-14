using Newtonsoft.Json;

namespace GraphVisual
{
    public class Milestones
    {
        [JsonProperty("milestonesTasks")]
        public Milestone[] milestones;
    }

    public class Milestone
    {
        [JsonProperty("id")]
        public string id;

        [JsonProperty("name")] 
        public string name;

        [JsonProperty("chain")] 
        public MilestoneTask[] chain;
        
    }

    public class MilestoneTask
    {
        [JsonProperty("name")] 
        public string id;

        [JsonProperty("parent")] 
        public string parentId;

        [JsonProperty("is_multi_parent")] 
        public bool isMultiParent;
    }
}