using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public EnemyDamager damager;
    public Projectile projectile;

    private float shotCounter;

    public float weaponRange;
    public LayerMask whatIsEnemy;

    private float spawnInterval = 0.05f;
    private int projectileSpawned = 0;
    private int projectileToSpawned;

    void Start()
    {
        SetStats();
    }


    void Update()
    {
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats();
        }

        if(PlayerController.instance.isDead == false)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter < 0)
            {
                shotCounter = stats[weaponLevel].timeBetweenAttacks;

                if (PlayerController.instance.awakeStat == true)
                    HandleShotMode();
                else
                    AutoAttackMode();
            }
        }

    }

    private void HandleShotMode()
    {
        spawnInterval = 0.1f;
        projectileSpawned = 0;
        projectileToSpawned = (int)stats[weaponLevel].amount + 2;
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets()
    {

        while (projectileSpawned < projectileToSpawned)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.z - Camera.main.orthographicSize;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);  //将鼠标屏幕坐标转换为世界坐标

            Vector3 direction = mouseWorldPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
            //SFXManager.instance.PlaySFXPitched(6);

            projectileSpawned++;

            // 等待指定的间隔时间再发射下一颗子弹
            yield return new WaitForSeconds(spawnInterval);

        }
    }

    private void AutoAttackMode()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
        if (enemies.Length > 0)
        {
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                Vector3 direction = targetPosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 90;
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);

            }
            //SFXManager.instance.PlaySFXPitched(6);
        }
    }

    void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;
        damager.lifeTime = stats[weaponLevel].duration;

        damager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        shotCounter = 0f;

        projectile.moveSpeed = stats[weaponLevel].speed;
    }
}
