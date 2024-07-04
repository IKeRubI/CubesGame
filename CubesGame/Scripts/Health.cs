using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamagable
{
    public static event EventHandler OnPlayerDie;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float healthRatio;
    }


    [SerializeField]
    private float healthMax;
    private float currentHealth;


    private void Start()
    {
        currentHealth = healthMax;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;

        if(currentHealth < 0) currentHealth = 0;

        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
        {
            healthRatio = currentHealth / healthMax
        });

        Debug.Log(currentHealth + "  " + healthMax);

        if (currentHealth == 0)
        {
            KillAgent();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
    }

    public void KillAgent()
    {
        if (TryGetComponent<Player>(out _))
        {
            OnPlayerDie?.Invoke(this, EventArgs.Empty);
        }

        Destroy(gameObject);
    }
}
