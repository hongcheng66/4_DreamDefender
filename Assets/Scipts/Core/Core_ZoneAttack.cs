using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_ZoneAttack : MonoBehaviour
{
    private Vector3 targetSize;
    public float growSpeed = 5f;
    public float rotationSpeed = 20f;

    public bool isAttack = false;
    public bool isSlow = false;

    public float damageAmount = 3f;
    public float damageBetweenTime = 1f;
    private float damageCounter;

    private List<Collider2D> eneimes = new List<Collider2D>();

    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        if(transform.localScale == targetSize)
        {
            float currentRotation = transform.rotation.eulerAngles.z;

            // 每秒增加旋转速度
            float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

            // 设置新的旋转角度
            transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }

        damageCounter -= Time.deltaTime;

        foreach(var enemy in eneimes)
        {
            if(isAttack)
            {
                if(damageCounter < 0)
                {
                    damageCounter = damageBetweenTime;
                    enemy.GetComponent<EnemyController>().TakeDamage(damageAmount);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().moveSpeed *= 1.1f;
        }
        if(collision.tag == "Enemy")
        {
            eneimes.Add(collision);

            if(isSlow)
            {
                collision.GetComponent<EnemyController>().moveSpeed *= 0.8f;
            }
        }

        if (collision.tag == "EnemyBullet")
        {
            if (isSlow)
            {
                collision.GetComponent<EnemyBulletController>().moveSpeed *= 0.5f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            eneimes.Remove(collision);
        }

        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().moveSpeed = collision.GetComponent<PlayerController>().moveSpeed / 1.1f;
        }
    }
}
