using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("ÊÂ¼þ¼àÌý")]
    public CharactEventSO healthEvent;
    public CharactEventSO MPEvent;

    private void OnEnable()
    {
        healthEvent.onEventRaised += OnHealthEvent;
        MPEvent.onEventRaised += OnMPEvent;
    }

    private void OnDisable()
    {
        healthEvent.onEventRaised -= OnHealthEvent;
        MPEvent.onEventRaised -= OnMPEvent;
    }

    private void OnMPEvent(Character character)
    {
        var persentage = character.currentMP / character.maxMP;    
        playerStatBar.OnMPChange(persentage);

        var currentMP = character.currentMP;
        var maxMP = character.maxMP;
        playerStatBar.MPText((int)currentMP, (int)maxMP);
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(persentage);

        var currentHealth = character.currentHealth;
        var maxHealth = character.maxHealth;
        playerStatBar.HealthText((int)currentHealth, (int)maxHealth);
    }
}
