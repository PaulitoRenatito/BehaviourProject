using System;
using TMPro;
using UnityEngine;
using Utilities;

namespace Cities
{
    public class CityUI : MonoBehaviour
    {
        [SerializeField] private City city;
        
        [SerializeField] private TextMeshProUGUI cityName;
        
        [SerializeField] private GameObject visitedIndicator;

        private void Start()
        {
            cityName.text = city.Name;
            
            city.OnVisited += City_OnVisited;
            
            Visibility.Hide(visitedIndicator);
        }

        private void City_OnVisited(object sender, EventArgs e)
        {
            Visibility.Show(visitedIndicator);
        }
    }
}
