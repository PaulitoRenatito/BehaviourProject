using System;
using Sounds;
using UnityEngine;

namespace Tasks
{
    public class TaskSound : MonoBehaviour
    {
        [SerializeField] private Task task;
        
        private SoundPlayer soundPlayer;
        
        private void Awake()
        {
            soundPlayer = GetComponent<SoundPlayer>();
            task.OnTaskStart += Task_OnTaskStart;
        }

        private void Task_OnTaskStart(object sender, EventArgs e)
        {
            Debug.Log("Task_OnTaskStart");
            soundPlayer.PlayLoop(task.TaskSo.TimeToComplete);
        }
    }
}
