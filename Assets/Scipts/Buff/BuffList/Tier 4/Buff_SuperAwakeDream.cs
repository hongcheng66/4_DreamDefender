using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Buff_SuperAwakeDream : Buff
{
    private GameObject target;

    private void OnEnable()
    {
        target = FindObjectsOfType<PlayerController>()
                              .Where(pc => pc.gameObject.CompareTag("Player"))
                              .FirstOrDefault()?.gameObject;
        target.GetComponent<PlayerController>().isAwakeDream = true;
        target.GetComponent<PlayerController>().switchCooldown = 30f;
    }

}
