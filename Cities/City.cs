using System;
using UnityEngine;

namespace Cities
{
    public class City : MonoBehaviour
    {
        public event EventHandler OnVisited;
        
        [SerializeField] private string name;
        [SerializeField] private Transform waypoint;

        [SerializeField] private bool visited;

        public string Name => name;
        public Transform Waypoint => waypoint;
        public bool Visited => visited;

        private void OnTriggerEnter(Collider other)
        {
            visited = true;
            OnVisited?.Invoke(this, EventArgs.Empty);
        }
    }
}
