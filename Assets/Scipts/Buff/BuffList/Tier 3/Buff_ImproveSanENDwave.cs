using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ImproveSanENDwave : Buff
{
    private void OnEnable()
    {
        EnemySpawner.instance.isNodensBuff = true;
    }
}
