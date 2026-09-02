using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Health")]
    public float MaxHealth;
    private float _curHealth;

    private void Awake()
    {
        _curHealth = MaxHealth;
    }
    public void TakeDamage(float amount)
    {
        _curHealth -= amount;
        OnTakeDamage();
        Debug.Log(_curHealth);
        if (_curHealth <= 0)
        {
            Debug.Log("DEAD");
            Death();
        }
    }
    public virtual void OnTakeDamage()
    {

    }
    public virtual void Death()
    {

    }
    public bool IsDead()
    {
        return _curHealth <= 0;
    }
}
