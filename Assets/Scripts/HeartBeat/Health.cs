using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int _maxHealth = 100;
    public int CurrentHealth;
    public HealthBar healthBar;
    private void Start()
    {
        CurrentHealth = _maxHealth;
    }
    public void DealDamage(int damage)
    {
        CurrentHealth-= damage;
        CheckHealth();
    }
    public void AddHealth(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
        CheckHealth();
    }
    void CheckHealth()
    {
        if (CurrentHealth >= 70)
        {
            healthBar.HealthStateNormal();
        }
        else if (CurrentHealth <= 70 && CurrentHealth >= 40)
        {
            healthBar.HealthStateCausious();
        }
        else if (CurrentHealth <= 40 && CurrentHealth > 0)
        {
            healthBar.HealthStateDanger();
        }
        else
        {
            healthBar.HealthStateDead();
            FindObjectOfType<GameManager>().Failed();
        }
    }


}
