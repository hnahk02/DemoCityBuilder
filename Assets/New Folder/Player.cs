using System.Collections;
using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private FloatReference currentHealth = null;
    [SerializeField]
    private FloatReference maxHealth = null;

    private void Awake()
    {
        currentHealth.Value = maxHealth.Value;
    }

    public void HealthChanged()
{
    if (currentHealth.Value < 0)
        Destroy(gameObject);
}
}
