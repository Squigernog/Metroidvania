using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int attack = 1;
    public int maxHealth = 100;
    [SerializeField]
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    /// <summary>
    /// Assign damage to the enemy
    /// </summary>
    /// <param name="damage">Damage from a player attack</param>
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (IsDead())
            Destroy(gameObject);
    }

    /// <summary>
    /// Checks if an enemy is dead. True if health < 0
    /// </summary>
    /// <returns>boolean</returns>
    private bool IsDead()
    {
        if (_currentHealth > 0)
            return false;
        else
            return true;
    }
}
