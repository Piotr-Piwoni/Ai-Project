using System;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthBar : MonoBehaviour
    {
        public Slider healthBar;

        private void Awake()
        {
            healthBar = GetComponent<Slider>();
        }

        public void AddHealth(float value)
        {
            healthBar.value += value;
        }
        
        public void RemoveHealth(float value)
        {
            healthBar.value -= value;
        }

        public void ChangeMaxHealth(float maxHealth)
        {
            healthBar.maxValue = maxHealth;
        }
    }
}
