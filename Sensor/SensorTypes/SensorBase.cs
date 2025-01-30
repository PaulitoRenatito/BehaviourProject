using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Sensor.SensorTypes
{
    public abstract class SensorBase<T> : MonoBehaviour where T : ISensorable
    {
        public event EventHandler<T> OnEnterSensor;
        public event EventHandler<T> OnExitSensor;

        private readonly SensorableManager<T> manager = new();

        private CountdownTimer timer;

        [SerializeField] private float radius;
        [SerializeField] private float detectInterval = 1f;
        
        [SerializeField] private bool debug = false;

        private void Start()
        {
            manager.OnEnterSensor += item =>
            {
                OnEnterSensor?.Invoke(this, item);
            };
            manager.OnExitSensor += item =>
            {
                OnExitSensor?.Invoke(this, item);
            };

            timer = new CountdownTimer(detectInterval);
            timer.OnTimerStop = () =>
            {
                UpdateSensor();
                timer.Start();
            };
            timer.Start();
        }

        private void Update() => timer.Tick(Time.deltaTime);

        private void UpdateSensor()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            List<T> detectedItems = new List<T>();

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out T item))
                {
                    detectedItems.Add(item);
                }
            }

            manager.UpdateSensorables(detectedItems);
        }

        protected void OnDrawGizmos()
        {
            if (!debug) return;
            
            Gizmos.color = manager.CurrentSensorables.Count <= 0 ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.color = Color.magenta;
            foreach (var sensorable in manager.CurrentSensorables)
            {
                Gizmos.DrawLine(transform.position, sensorable.GetTransform().position);
            }
        }
    }
}
