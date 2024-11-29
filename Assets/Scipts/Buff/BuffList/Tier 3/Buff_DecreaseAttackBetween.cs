using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_DecreaseAttackBetween : Buff
{
    public GameObject core_zone;


    private void OnEnable()
    {
        core_zone.SetActive(true);

        core_zone.GetComponent<Core_ZoneAttack>().damageBetweenTime = 0.5f;
    }
}
