using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_KillEnemyRecoverII : Buff
{
    public GameObject lowLevel;
    private void Update()
    {
        foreach (var enemy in EnemySpawner.instance.spawnedEnemies)
        {
            EnemyController script = enemy.GetComponent<EnemyController>();
            script.isBackHealth = true;
            script.backHealthAmount = 3f;
        }
    }

    private void OnEnable()
    {
        lowLevel.SetActive(false);
    }
}
