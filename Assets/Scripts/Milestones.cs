using System;
using System.Collections.Generic;

namespace GraphVisual
{
    /*[Serializable]
    public class Milestones
    {
        public List<Milestone> milestones;
    }*/

    [Serializable]
    public class Milestone
    {
        //[JsonProperty("id")]
        public string id;

        //[JsonProperty("name")] 
        public string name;

        public List<MilestoneTask> chain;
    }

    [Serializable]
    public class MilestoneTask
    {
        //[JsonProperty("name")] 
        public string id;

        //[JsonProperty("parent")] 
        public string parentId;

        //[JsonProperty("is_multi_parent")] 
        public bool isMultiParent;
    }
}