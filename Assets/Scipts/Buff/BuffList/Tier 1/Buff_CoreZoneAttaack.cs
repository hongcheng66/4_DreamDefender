using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_CoreZoneAttaack : Buff
{
    public GameObject core_zoneAttack;
    private void OnEnable()
    {
        core_zoneAttack.SetActive(true);
        core_zoneAttack.GetComponent<Core_ZoneAttack>().isAttack = true;
    }
}
