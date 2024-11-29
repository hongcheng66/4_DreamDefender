using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ImproveCoreDamage : Buff
{
    public GameObject core_zone;
    private void OnEnable()
    {
        core_zone.SetActive(true);

        core_zone.GetComponent<Core_ZoneAttack>().damageAmount = 5f;
    }
}
