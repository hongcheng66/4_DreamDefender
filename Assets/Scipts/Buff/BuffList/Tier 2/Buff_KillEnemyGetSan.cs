using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_KillEnemyGetSan : Buff
{
    private void Update()
    {
        foreach(var enemy in EnemySpawner.instance.spawnedEnemies)
        {
            EnemyController script = enemy.GetComponent<EnemyController>();
            script.isBackSan = true;
            script.backSanAmount = 1f;
        }
    }
}
