using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Task
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
        }

        private void TaskOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            bool show = e.progressNormalized > 0;

            if (!show) Visibility.Hide(progressIndicator);
            
            Visibility.Show(progressIndicator);
            barImage.fillAmount = e.progressNormalized;
        }
    }
}
