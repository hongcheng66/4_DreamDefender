using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightZone : MonoBehaviour
{
    private float range;
    private float healAmount;
    private float healBetweenTime = 2f;
    [SerializeField]private float healCounter;

    // Start is called before the first frame update
    void Start()
    {
        ZoneWeapon scrpit = GetComponentInParent<ZoneWeapon>();
        range = scrpit.stats[scrpit.weaponLevel].range * 10f;
        healAmount = scrpit.stats[scrpit.weaponLevel].damage;

        healCounter = healBetweenTime;
    }

    // Update is called once per frame
    void Update()
    {
        healCounter -= Time.deltaTime;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);

        foreach(var collider in colliders)
        {
            if(collider.tag == "Player")
            {
                if (healCounter < 0)
                {
                    collider.GetComponent<PlayerHealthController>().AddHealth(healAmount);
                    healCounter = healBetweenTime;
                }
            }
        }
    }
}
