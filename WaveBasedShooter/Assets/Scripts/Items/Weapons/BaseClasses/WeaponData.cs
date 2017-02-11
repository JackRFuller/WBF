﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string weaponName;
    public string WeaponName
    {
        get
        {
            return weaponName;
        }
    }

    [SerializeField]
    private WeaponType.Type weaponType;
    public WeaponType.Type WeaponType
    {
        get
        {
            return weaponType;
        }
    }

    [SerializeField]
    private Sprite weaponIcon;
    public Sprite WeaponIcon
    {
        get
        {
            return weaponIcon;
        }
    }

    [SerializeField]
    private GameObject weaponPrefab;
    public GameObject WeaponPrefab
    {
        get
        {
            return weaponPrefab;
        }
    }

    [SerializeField]
    private float maxWeaponHealth = 100;
    public float MaxWeaponHealth
    {
        get
        {
            return maxWeaponHealth;
        }
    }

    [Header("Sprint Attack Attributes")]
    [SerializeField]
    private float sprintAttackDamage;
    public float SprintAttackDamage
    {
        get
        {
            return sprintAttackDamage;
        }
    }

    private float sprintAttackStaminaCost;
    public float SprintAttackStaminaCost
    {
        get
        {
            return sprintAttackDamage;
        }
    }

    [Header("Standard Attack Attributes")]
    [SerializeField]
    private float[] standardAttackDamage;
    public float[] StandardAttackDamage
    {
        get
        {
            return standardAttackDamage;
        }
    }
}
