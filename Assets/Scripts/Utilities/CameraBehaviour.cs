using System;
using Sensor.SensorTypes;
using Tasks;
using Unity.Cinemachine;
using UnityEngine;

namespace Utilities
{
    public class CameraBehaviour : MonoBehaviour
    {
        private CinemachineCamera camera;
        private Transform target;
        
        [SerializeField] SensorTask sensorTask;

        private void Awake()
        {
            camera = GetComponent<CinemachineCamera>();
            target = camera.Follow;
        }
        
        private void Start()
        {
            sensorTask.OnEnterSensor += SensorTask_OnEnterSensor;
            sensorTask.OnExitSensor += SensorTask_OnExitSensor;
        }
        
        private void SensorTask_OnEnterSensor(object sender, Task e)
        {
            camera.Follow = e.transform;
        }
        
        private void SensorTask_OnExitSensor(object sender, Task e)
        {
            camera.Follow = target;
        }
    }
}
