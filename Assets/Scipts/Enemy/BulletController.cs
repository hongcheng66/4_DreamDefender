using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damageAmount;

    public float lifeTime;

    public bool destroyParent;

    public bool damageOverTime;
    public float timeBetweenDamage;

    public bool isStrike; //�Ӵ����Ƿ��ƻ��ӵ� ���������������ӵ��Ƿ�͸��


    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(gameObject);

            if (destroyParent)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Player")
       {
           collision.GetComponent<PlayerHealthController>().TakeDamage(damageAmount);

           if(!isStrike)
           {
               Destroy(gameObject);
            }
            
       }
    }
}
