using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : BaseMonoBehaviour
{
    [SerializeField]
    private WeaponData weapon;
    [SerializeField]
    private Transform weaponSpawnPoint;
    private GameObject weaponObject;

    private void Start()
    {
        weaponObject = Instantiate(weapon.WeaponPrefab) as GameObject;

        weaponObject.transform.parent = this.transform;

        weaponObject.transform.position = weaponSpawnPoint.position;
        weaponObject.transform.rotation = weaponSpawnPoint.rotation;


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {

        }
    }

}
