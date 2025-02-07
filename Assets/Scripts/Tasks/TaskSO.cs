using UnityEngine;

namespace Tasks
{
    [CreateAssetMenu(fileName = "TaskSO", menuName = "ScriptableObjects/Task", order = 1)]
    public class TaskSO : ScriptableObject
    {
        public string Name;
        
        public int Profit;
        public int Health;
        public int Stamina;
        public int TimeToComplete;
    }
}
