using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buff_KillEnemyRecover : Buff
{
    private GameObject target;

    private void OnEnable()
    {
        target = FindObjectsOfType<PlayerController>()
                             .Where(pc => pc.gameObject.CompareTag("Player"))
                             .FirstOrDefault()?.gameObject;

        target.GetComponent<PlayerController>().isKillRecover = true;

    }
}
