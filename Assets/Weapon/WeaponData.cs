using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] private PlatformerInteraction pi;

    public WeaponDatabase weapon;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.Find("Player 1").GetComponent<PlatformerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (pi.interactItem)
            {
                Destroy(pi.interactedItem);
                EquipWeapon();
            }
        }
    }

    private void EquipWeapon()
    {
        switch (weapon.weaponType)
        {
            case WeaponType.Axe:
                break;

            case WeaponType.Spear:
                break;

            case WeaponType.Sword:
                break;
        }
    }
}
