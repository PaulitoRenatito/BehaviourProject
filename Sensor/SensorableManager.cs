using System;
using System.Collections.Generic;
using System.Linq;

namespace Sensor
{
    public class SensorableManager<T>
    {

        private readonly HashSet<T> previousSensorables = new();
        private readonly HashSet<T> currentSensorables = new();

        public HashSet<T> CurrentSensorables => currentSensorables;

        public event Action<T> OnEnterSensor;
        public event Action<T> OnExitSensor;
    
        public void UpdateSensorables(IEnumerable<T> detectedSensorables)
        {
            currentSensorables.Clear();
            foreach (T sensorable in detectedSensorables)
            {
                currentSensorables.Add(sensorable);

                if (!previousSensorables.Contains(sensorable))
                {
                    OnEnterSensor?.Invoke(sensorable);
                }
            }

            var removedSensorables = previousSensorables.Except(currentSensorables).ToHashSet();

            foreach (var removedSensorable in removedSensorables)
            {
                OnExitSensor?.Invoke(removedSensorable);
            }

            UpdatePreviousSensorables();
        }
    
        private void UpdatePreviousSensorables()
        {
            previousSensorables.Clear();
            foreach (T sensorable in currentSensorables)
            {
                previousSensorables.Add(sensorable);
            }
        }
    }
}
