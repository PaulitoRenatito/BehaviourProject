using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Tasks
{
    public class TaskUI : MonoBehaviour
    {
        [SerializeField] private Task task;
        
        [SerializeField] private TextMeshProUGUI nameText;
        
        [SerializeField] private GameObject progressIndicator;
        [SerializeField] private Image barImage;
        
        private void Start()
        {
            nameText.text = task.Name;
            
            task.OnProgressChanged += TaskOnProgressChanged;

            barImage.fillAmount = 0f;

            Visibility.Hide(progressIndicator);
            
            GameManager.Instance.OnReset += GameManager_OnReset;
        }

        private void TaskOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            bool show = e.progressNormalized > 0 && e.progressNormalized < 0.99f;

            if (!show)
            {
                Visibility.Hide(progressIndicator);
                return;
            }
            
            Visibility.Show(progressIndicator);
            barImage.fillAmount = e.progressNormalized;
        }
        
        private void GameManager_OnReset(object sender, EventArgs e)
        {
            barImage.fillAmount = 0f;

            Visibility.Hide(progressIndicator);
        }
    }
}
