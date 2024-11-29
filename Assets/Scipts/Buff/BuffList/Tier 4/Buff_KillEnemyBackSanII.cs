using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_KillEnemyBackSanII : Buff
{
    public GameObject lowLevel;

    private void Update()
    {
        foreach (var enemy in EnemySpawner.instance.spawnedEnemies)
        {
            EnemyController script = enemy.GetComponent<EnemyController>();
            script.isBackSan = true;
            script.backSanAmount = 2f; ;
        }
    }

    private void OnEnable()
    {
        lowLevel.SetActive(false);
    }
}
