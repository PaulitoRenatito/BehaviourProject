using System;
using UnityEngine;

namespace Behaviour.Blackboard
{
    public class BlackboardTask
    {
        public event EventHandler<float> OnHealthChanged;
        public event EventHandler<float> OnStaminaChanged;
        public event EventHandler<int> OnCurrencyChanged;

        private float health = 100;
        private float stamina = 100;
        private int profit = 0;
        private float elapsedTime = 0;

        public float Health
        {
            get => health;
            set
            {
                if (Mathf.Approximately(health, value)) return;
                health = value;
                OnHealthChanged?.Invoke(this, health);
            }
        }

        public float Stamina
        {
            get => stamina;
            set
            {
                if (Mathf.Approximately(stamina, value)) return;
                stamina = value;
                OnStaminaChanged?.Invoke(this, stamina);
            }
        }

        public int Profit
        {
            get => profit;
            set
            {
                if (profit == value) return;
                profit = value;
                OnCurrencyChanged?.Invoke(this, profit);
            }
        }

        public float ElapsedTime
        {
            get => elapsedTime;
            set => elapsedTime = value;
        }
    }
}
