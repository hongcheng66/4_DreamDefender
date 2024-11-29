using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buff_BurningBlood : Buff
{
    private GameObject target;

    private void OnEnable()
    {
        target = FindObjectsOfType<PlayerController>()
                             .Where(pc => pc.gameObject.CompareTag("Player"))
                             .FirstOrDefault()?.gameObject;

        target.GetComponent<PlayerHealthController>().isBurningBlood = true;
        target.GetComponent<PlayerController>().moveSpeed *= 2;

    }
}
