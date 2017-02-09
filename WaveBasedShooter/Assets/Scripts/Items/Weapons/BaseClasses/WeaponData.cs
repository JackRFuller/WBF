using System.Collections;
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

    [SerializeField]
    private float[] damageInflicted;
    public float[] DamageInflicted
    {
        get
        {
            return damageInflicted;
        }
    }
}
