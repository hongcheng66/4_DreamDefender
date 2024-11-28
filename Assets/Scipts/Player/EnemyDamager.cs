using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;

    public bool isIndepend;

    public float lifeTime,growSpeed = 5f;
    private Vector3 targetSize;

    public bool shouldKnockBack;

    public bool destroyParent;

    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;

    private List<EnemyController> enemiesInRange = new List<EnemyController>();

    public bool destroyOnImpact;

    public bool canDefendBullet = false;


    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, lifeTime);

        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale,targetSize,growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        //判断消除投掷物的buff有没有被激活
        if(gameObject.tag == "DefendBulletWeapon")
        {
            Buff_DefendBullet buff = FindObjectOfType<Buff_DefendBullet>();
            if (buff != null)
                canDefendBullet = true;
        }

        if(lifeTime <= 0 && isIndepend == false)
        {
            targetSize = Vector3.zero;

            if(transform.localScale.x == 0f)
            {
                Destroy(gameObject);

                if(destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        if(damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;

            if(damageCounter <= 0)
            {
                damageCounter = timeBetweenDamage;

                for(int i = 0; i < enemiesInRange.Count;i++)
                {
                    if(enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount,shouldKnockBack);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canDefendBullet == true)
        {
            if(collision.tag == "EnemyBullet")
            {
                Destroy(collision.gameObject);
            }
        }

        if(damageOverTime == false)
        {
            if(collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().TakeDamage(damageAmount,shouldKnockBack);

                if(destroyOnImpact)
                {
                    Destroy(gameObject);
                }
            }
            if(collision.tag == "Treasure")
            {
                collision.GetComponent<Enemy_TreasureBox>().TakeDamage(damageAmount);
            }
        }
        else
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<EnemyController>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(damageOverTime == true)
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }

}
