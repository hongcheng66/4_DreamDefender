using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Enemy_TreasureBox : MonoBehaviour
{
    public Animator anim;

    public Rigidbody2D theRB;
    public float moveSpeed;

    public float damage;

    public float hitWaitTime = 1f;
    private float hitCounter;

    public float health = 5f;

    public float knockBackTime = .5f;
    private float knockBackCounter;

    public SpriteRenderer sprite;
    public float waitTime = 1f;

    public int expToGive = 1;

    public bool isalive = true;
    private bool isappear = false;
    private bool isRunningaway = false;

    public float runningTime = 5f;
    private float runningCounter;

    private GameObject target;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {

        health = 10f * (EnemySpawner.instance.currentWave + 1);
        isalive = true;
        runningCounter = runningTime;
        StartCoroutine(ChangeAlpha(new Color(1, 1, 1, 0), Color.white, waitTime)); //ʵ��͸������Ч��
        target = FindObjectsOfType<PlayerController>()
                            .Where(pc => pc.gameObject.CompareTag("Player"))
                            .FirstOrDefault()?.gameObject;
        player = target.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isappear)
        {
            if (target.activeSelf == true)
            {
                if (knockBackCounter > 0)  //�����ж�
                {
                    knockBackCounter -= Time.deltaTime;

                    if (moveSpeed > 0)
                    {
                        moveSpeed = -moveSpeed * 2f;
                    }

                    if (knockBackCounter <= 0)
                    {
                        moveSpeed = Mathf.Abs(moveSpeed * .5f);  //��ԭ�����˵��ٶ�
                    }
                }

                theRB.velocity = (target.transform.position - transform.position).normalized * moveSpeed;

                if (isRunningaway == true)
                {
                    theRB.velocity = -theRB.velocity;
                    runningCounter -= Time.deltaTime;
                }

                if (runningCounter <= 0)
                    Destroy(gameObject);
            }
            else
            {
                theRB.velocity = Vector2.zero;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Core")
        {
            CoreSanController.instance.currentSan--;
            isalive = false;
            Destroy(gameObject);
        }
    }


    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0 && isalive == true)
        {
            isalive = false;
            moveSpeed = 0f;

            StartCoroutine(ChangeAlpha(Color.white, new Color(1, 1, 1, 0), waitTime));

            anim.SetBool("isdead", true);

            SFXManager.instance.PlaySFXPitched(0);//����������Ч
        }
        else //�ܵ���������ֿ�ʼ����
        {
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
            isRunningaway = true;
            moveSpeed = 2f;
            //SFXManager.instance.PlaySFXPitched(1); //�����ܻ���Ч
        }

        if (isalive == true)
            DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        if (isalive == true)
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
        {
            BuffController.instance.UpgradeBuffPanel();
            Destroy(gameObject);
        }
    }
}
