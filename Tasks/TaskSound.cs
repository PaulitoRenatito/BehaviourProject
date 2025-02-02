using System;
using Managers;
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

        private void Start()
        {
            GameManager.Instance.OnReset += GameManager_OnReset;
            GameManager.Instance.OnPause += GameManager_OnPause;
            
            soundPlayer?.Stop();
        }

        private void OnEnable()
        {
            soundPlayer?.Stop();
        }

        private void Task_OnTaskStart(object sender, EventArgs e)
        {
            Debug.Log("Task_OnTaskStart");
            soundPlayer.PlayLoop(task.TaskSo.TimeToComplete);
        }
        
        private void GameManager_OnReset(object sender, EventArgs e)
        {
            soundPlayer?.Stop();
        }
        
        private void GameManager_OnPause(object sender, EventArgs e)
        {
            soundPlayer?.Stop();
        }
    }
}
