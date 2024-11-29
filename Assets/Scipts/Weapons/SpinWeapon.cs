using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpinWeapon : Weapon
{
    public float Speed = 180f;

    public Transform holder,fireballToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;

    public EnemyDamager damager;
    public FlyingSword flyingSword;
    private int amount;

    private GameObject target;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();

        target = FindObjectsOfType<PlayerController>()
                            .Where(pc => pc.gameObject.CompareTag("Player"))
                            .FirstOrDefault()?.gameObject;
        player = target.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isDead == false)
        {
            if (PlayerController.instance.awakeStat == true)
            {
                if(player.dreamComeTrue == false)
                    amount = 0;

                StartCoroutine(RoundMode());
            }
            else
            {
                if(amount <= stats[weaponLevel].amount)
                {
                    StartCoroutine(AutoFlyingMode());
                }
            }

        }



        if (statsUpdated)
        {
            statsUpdated = false;

            SetStats();
        }
    }

    private IEnumerator AutoFlyingMode()
    {
        amount++;

        yield return new WaitForSeconds(1f);

        flyingSword.moveSpeed = stats[weaponLevel].speed + 2f;
        flyingSword.duration = stats[weaponLevel].duration + 10f;
        flyingSword.searchRadius = stats[weaponLevel].range;

        Instantiate(flyingSword, transform.position, Quaternion.identity).gameObject.SetActive(true);
    }

    private IEnumerator RoundMode()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (Speed * Time.deltaTime * stats[weaponLevel].speed));

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            //Instantiate(fireballToSpawn,fireballToSpawn.position,fireballToSpawn.rotation,holder).gameObject.SetActive(true);
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;

                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);

                SFXManager.instance.PlaySFX(8);

            }
        }

        yield return null;
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;

        transform.localScale = Vector3.one;

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0f;
    }

}
