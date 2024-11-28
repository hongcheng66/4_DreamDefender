using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_StrongHealth : Buff
{
    public float improveHealthRate = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerHealthController.instance.ChangeHealthRate(improveHealthRate);
    }
}
