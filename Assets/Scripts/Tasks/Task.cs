using System;
using Managers;
using Sensor;
using UnityEngine;
using Utilities;

namespace Tasks
{
    public class Task : MonoBehaviour, IHasProgress, ISensorable
    {
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnTaskStart;
        public event EventHandler OnTaskComplete;
        
        [SerializeField] private TaskSO taskSo;
        [SerializeField] private Transform waypoint;
        [SerializeField] private bool debug = false;
        
        public string Name => taskSo.Name;
        public int TimeToComplete => taskSo.TimeToComplete;
        public TaskSO TaskSo => taskSo;
        
        public Transform Waypoint => waypoint;
        
        private Timer timer;
        
        private void Start()
        {
            timer = new CountdownTimer(taskSo.TimeToComplete);
            
            timer.OnTimerStart += () =>
            {
                OnTaskStart?.Invoke(this, EventArgs.Empty);
                if (debug) Debug.Log($"{taskSo.Name} starting...");
            };

            timer.OnTimerStop += () =>
            {
                OnTaskComplete?.Invoke(this, EventArgs.Empty);
                if (debug) Debug.Log($"{taskSo.Name} complete!");
            };
            
            GameManager.Instance.OnReset += GameManager_OnReset;
        }

        public void StartTask()
        {
            if (!timer.IsRunning)
            {
                timer.Start();
            }
        }

        private void Update()
        {
            if (!timer.IsRunning) return;
            
            timer.Tick(Time.deltaTime);
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = 1 - timer.Progress
            });
        }

        public Transform GetTransform()
        {
            return this.transform;
        }
        
        private void GameManager_OnReset(object sender, EventArgs e)
        {
            timer.Stop();
        }
    }
}
