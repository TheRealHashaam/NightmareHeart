using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorTree 
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node : MonoBehaviour
    {
        protected NodeState state;
        public Node Parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string,object> data = new Dictionary<string,object>();

        public Node()
        {
            Parent = null;
        }
        public Node(List<Node> children)
        {
            foreach (Node n in children)
            {
                _Attach(n);
            }
        }

        private void _Attach(Node node)
        {
            node.Parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE; 

        public void SetData(string key, object value)
        {
            data[key] = value;
        }

        public object GetData(string key) 
        {
            object value = null;
            if(data.TryGetValue(key, out value))
            {
                return value;
            }
            Node node = Parent;

            while(node != null)
            {
                value = node.GetData(key);
                if(value != null)
                {
                    return value;
                }
                node = node.Parent;
            }
            return null;
        }
        public object ClearData(string key)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
                return true;
            }
            Node node = Parent;

            while (node != null)
            {
                bool cleared = (bool)node.ClearData(key);
                if(cleared)
                {
                    return true;
                }
                node = node.Parent;
            }
            return false;
        }

    }
}

