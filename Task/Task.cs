using System;
using UnityEngine;
using Utilities;

namespace Task
{
    public class Task : MonoBehaviour, IHasProgress
    {
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        
        [SerializeField] private TaskSO taskSo;

        public string Name => taskSo.Name;

        [SerializeField] private int completed = 0;
        private Timer timer;
        // private Timer timer2;

        private Collider collider;
        
        private void Start()
        {
            collider = GetComponent<Collider>();
            
            timer = new CountdownTimer(taskSo.TimeToComplete);
            
            timer.OnTimerStart += () =>
            {
                Debug.Log($"{taskSo.Name} starting...");
            };

            timer.OnTimerStop += () =>
            {
                completed++;
                Debug.Log($"{taskSo.Name} complete!");
            };
        }

        private void OnTriggerStay(Collider other)
        {
            // timer2 = null;
            
            if (!timer.IsRunning)
            {
                timer.Start();
            }
            
            timer.Tick(Time.deltaTime);
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = 1 - timer.Progress
            });
        }

        // private void OnTriggerExit(Collider other)
        // {
        //     timer2 = new CountdownTimer(timer.Progress * taskSo.TimeToComplete);
        //     
        //     timer2.OnTimerStart += () =>
        //     {
        //         Debug.Log($"{taskSo.Name} starting...");
        //     };
        //
        //     timer2.OnTimerStop += () =>
        //     {
        //         Debug.Log($"{taskSo.Name} complete!");
        //     };
        // }
        //
        // private void Update()
        // {
        //     if (timer2 == null) return;
        //     
        //     timer2.Tick(Time.deltaTime);
        //     OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        //     {
        //         progressNormalized = timer2.Progress
        //     });
        // }
    }
}
