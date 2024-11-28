using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ImproveMaxWeapon : Buff
{
    private void OnEnable()
    {
        PlayerController.instance.maxWeapons++;
    }
}
