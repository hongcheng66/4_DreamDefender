using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_resetReviveTime : Buff
{
    private void OnEnable()
    {
        PlayerHealthController.instance.isMedical = true;
    }
}
