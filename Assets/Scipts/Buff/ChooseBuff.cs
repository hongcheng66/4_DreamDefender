using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuff : MonoBehaviour
{
    public static ChooseBuff instance;

    private void Awake()
    {
        instance = this;
    }

    public BuffChooseButton[] buffChooseButtons;
}
