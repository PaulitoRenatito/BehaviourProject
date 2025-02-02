using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public class EntityUI : MonoBehaviour
    {
        [SerializeField] private EntityTask entity;
        
        [SerializeField] private TextMeshProUGUI nameText;
        
        [SerializeField] private Image healthBarImage;
        [SerializeField] private Image staminaBarImage;
        
        [SerializeField] private TextMeshProUGUI currencyText;
        
        private void Start()
        {
            nameText.text = entity.Name;
            
            entity.BlackboardTask.OnHealthChanged += EntityOnHealthChanged;
            entity.BlackboardTask.OnStaminaChanged += EntityOnStaminaChanged;
            entity.BlackboardTask.OnCurrencyChanged += EntityOnCurrencyChanged;

            healthBarImage.fillAmount = 1f;
            staminaBarImage.fillAmount = 1f;
            currencyText.text = "0";
        }

        private void EntityOnHealthChanged(object sender, float e)
        {
            healthBarImage.fillAmount = e / 100;
        }
        
        private void EntityOnStaminaChanged(object sender, float e)
        {
            staminaBarImage.fillAmount = e / 100;
        }
        
        private void EntityOnCurrencyChanged(object sender, int e)
        {
            currencyText.text = e.ToString();
        }
    }
}
