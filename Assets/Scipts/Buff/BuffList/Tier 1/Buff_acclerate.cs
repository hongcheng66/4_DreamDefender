using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_acclerate : Buff
{
    public float improveSpeed = 0.5f;
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
        PlayerController.instance.ChangeSpeed(improveSpeed);
    }
}
