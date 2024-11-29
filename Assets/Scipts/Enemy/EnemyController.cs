using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    private float originSpeed;
    public float moveSpeed;
    private GameObject target;
    private GameObject player;

    public float damage;

    public float hitWaitTime = 1f;
    private float hitCounter;

    public float health = 5f;

    public float knockBackTime = .5f;
    private float knockBackCounter;

    public SpriteRenderer sprite;
    public float waitTime = 1f;

    public int expToGive = 1;

    public int coinValue = 1;
    public float coinDropRate = .5f;

    public bool isalive = true;
    private bool isappear = false;

    private float damageRate = 1f;

    [Header("Kill Enemy Recover San")]
    public bool isBackSan = false;
    public float backSanAmount = 0f;

    [Header("Kill Enemy Recover Health")]
    public bool isBackHealth = false;
    public float backHealthAmount = 0f;

    [Header("Positive Enemy")]
    public bool isPositive = false;

    [Header("Archer")]
    public bool isArcher = false;
    public GameObject bullet;
    private GameObject aimTarget;
    private float shotCounter;
    public float timeBetweenAttacks;
    public float bulletFlyingTime;
    public float bulletDamage;
    public bool isStrike;

    private bool CarterTrick = false;

    // Start is called before the first frame update
    void Start()
    {
        originSpeed = moveSpeed;
        damageRate = 1f;

        isalive = true;
        StartCoroutine(ChangeAlpha(new Color(1, 1, 1, 0), Color.white, waitTime)); //实现透明出现效果

        if(isPositive == false)
        {
            if(FindObjectOfType<CoreController>().gameObject != null)
            {
                target = FindObjectOfType<CoreController>().gameObject;
            }
        }
        else
        {
            target = FindObjectsOfType<PlayerController>()
                              .Where(pc => pc.gameObject.CompareTag("Player"))
                              .FirstOrDefault()?.gameObject;
        }

        player = FindObjectsOfType<PlayerController>()
                              .Where(pc => pc.gameObject.CompareTag("Player"))
                              .FirstOrDefault()?.gameObject;


        if (isArcher)
        {
            aimTarget = FindObjectsOfType<PlayerController>()
                              .Where(pc => pc.gameObject.CompareTag("Player"))
                              .FirstOrDefault()?.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isappear)
        {
            if(target != null)
            {
                if (target.activeSelf == true)
                {
                    if(player.GetComponent<PlayerController>().isAwakeDream == true) //buff
                    {
                        CarterTrick = true;
                    }

                    if(player.GetComponent<PlayerController>().isBurningSan == true)
                    {
                        damageRate += (CoreSanController.instance.maxSan - CoreSanController.instance.currentSan) * 0.02f;
                    }

                    if(player.GetComponent<PlayerController>().isKillRecover == true)
                    {
                        isBackHealth = true;
                        backHealthAmount = 1f;
                    }

                    if (player.GetComponent<PlayerController>().isKillBackSan == true)
                    {
                        isBackSan = true;
                        backSanAmount = 1f;
                    }

                    if (knockBackCounter > 0)  //击退判定
                    {
                        knockBackCounter -= Time.deltaTime;

                        if (moveSpeed > 0)
                        {
                            moveSpeed = -moveSpeed * 2f;
                        }

                        if (knockBackCounter <= 0)
                        {
                            moveSpeed = Mathf.Abs(moveSpeed * .5f);  //还原被击退的速度
                        }
                    }
                    theRB.velocity = (target.transform.position - transform.position).normalized * moveSpeed;

                    if (hitCounter > 0f)
                    {
                        hitCounter -= Time.deltaTime;
                    }

                    if(isArcher)
                    {
                        shotCounter -= Time.deltaTime;

                        if (shotCounter < 0)
                        {
                            shotCounter = timeBetweenAttacks;

                            Vector3 direction = aimTarget.transform.position - transform.position;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            angle -= 90;
                            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                            bullet.GetComponent<BulletController>().lifeTime = bulletFlyingTime;
                            bullet.GetComponent<BulletController>().damageAmount = bulletDamage;
                            bullet.GetComponent<BulletController>().isStrike = isStrike;

                            Instantiate(bullet, transform.position, bullet.transform.rotation).gameObject.SetActive(true);
                        }
                    }

                }
                else
                {
                    theRB.velocity = Vector2.zero;
                }
            }
            else
            {
                if (isPositive == false)
                {
                    target = FindObjectOfType<CoreController>().gameObject;
                }
                else
                {
                    target = FindObjectOfType<PlayerController>().gameObject;
                }
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f && isalive == true)
        {
            PlayerHealthController.instance.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
        if (collision.gameObject.tag == "Core")
        {
            CoreSanController.instance.currentSan -= damage;
            isalive = false;
            StartCoroutine(ChangeAlpha(Color.white, new Color(1, 1, 1, 0), waitTime));
        }
    }


    public void TakeDamage(float damageToTake)
    {
        if (CarterTrick == true && player.GetComponent<PlayerController>().awakeStat == true)
            health -= damageToTake * damageRate * 1.2f;
        else
            health -= damageToTake * damageRate;

        if (health <= 0 && isalive == true)
        {
            isalive = false;
            moveSpeed = 0f;
            StartCoroutine(ChangeAlpha(Color.white, new Color(1, 1, 1, 0), waitTime)); //利用协程使敌人消失后销毁

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
            /*  金币掉落 现已删除
            if (Random.value <= coinDropRate)
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
            */

            if(isBackSan == true)
            {
                CoreSanController.instance.AddSan(backSanAmount);
            }

            if(isBackHealth == true)
            {
                PlayerHealthController.instance.AddHealth(backHealthAmount);
            }

            SFXManager.instance.PlaySFXPitched(0);//敌人死亡音效
        }
        else
        {
            //SFXManager.instance.PlaySFXPitched(1); //敌人受击音效
        }

        if (isalive == true)
            DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        if(isalive == true)
        {
            TakeDamage(damageToTake);

            if (shouldKnockback)
            {
                knockBackCounter = knockBackTime;
            }
        }
    }

    IEnumerator ChangeAlpha(Color startColor, Color endColor, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            sprite.color = Color.Lerp(startColor, endColor, (Time.time - startTime) / duration);
            yield return null;
        }

        if (isappear == false)
            isappear = true;

        if (isalive == false)
            Destroy(gameObject);
    }

}
