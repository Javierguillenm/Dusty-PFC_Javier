using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons")]
public class WeaponsSO : ScriptableObject
{
    [SerializeField] public int DMG;
    [SerializeField] public int durability;
    [SerializeField] public int maxDurability;

    private void OnEnable()
    {
        durability = maxDurability;
    }

}
