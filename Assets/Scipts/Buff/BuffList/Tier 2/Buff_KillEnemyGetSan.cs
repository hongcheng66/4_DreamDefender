using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buff_KillEnemyGetSan : Buff
{
    private GameObject target;

    private void OnEnable()
    {
        target = FindObjectsOfType<PlayerController>()
                             .Where(pc => pc.gameObject.CompareTag("Player"))
                             .FirstOrDefault()?.gameObject;

        target.GetComponent<PlayerController>().isKillBackSan = true;

    }
}
