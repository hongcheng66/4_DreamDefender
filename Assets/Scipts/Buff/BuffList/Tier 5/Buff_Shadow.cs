using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Shadow : Buff
{
    private GameObject player;
    private GameObject shadow;
    private int amount = 1;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().awakeStat == false && amount > 0)
        {
            shadow = Instantiate(player,transform.position, Quaternion.identity);
            shadow.tag = "Fake";
            amount--;
        }
        if(player.GetComponent<PlayerController>().awakeStat == true)
        {
            amount = 1;
            Destroy(shadow);
        }
    }
}
