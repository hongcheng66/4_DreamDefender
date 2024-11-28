using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_ImproveMaxHealthII : Buff
{
    public float improveMaxHealth = 50;
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
        PlayerHealthController.instance.ChangeMaxHealth(improveMaxHealth);
    }
}
