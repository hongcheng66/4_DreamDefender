using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ImproveEXPGet : Buff
{
    private void OnEnable()
    {
        ExperienceLevelController.instance.addition += 1;
    }
}
