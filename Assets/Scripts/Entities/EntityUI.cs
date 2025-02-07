using System;
using Managers;
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
        [SerializeField] private Image timeIndicatorImage;
        
        [SerializeField] private TextMeshProUGUI currencyText;
        
        private void Start()
        {
            nameText.text = entity.Name;
            
            entity.BlackboardTask.OnHealthChanged += EntityOnHealthChanged;
            entity.BlackboardTask.OnStaminaChanged += EntityOnStaminaChanged;
            entity.BlackboardTask.OnCurrencyChanged += EntityOnCurrencyChanged;
            entity.BlackboardTask.OnElapsedTimeChanged += EntityOnElapsedTimeChanged;

            healthBarImage.fillAmount = 1f;
            staminaBarImage.fillAmount = 1f;
            timeIndicatorImage.fillAmount = 0f;
            currencyText.text = "0";
            
            GameManager.Instance.OnReset += GameManager_OnReset;
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
        
        private void EntityOnElapsedTimeChanged(object sender, float e)
        {
            timeIndicatorImage.fillAmount = e / 60f;
        }
        
        private void GameManager_OnReset(object sender, EventArgs e)
        {
            healthBarImage.fillAmount = 1f;
            staminaBarImage.fillAmount = 1f;
            timeIndicatorImage.fillAmount = 0f;
            currencyText.text = "0";
        }
    }
}
