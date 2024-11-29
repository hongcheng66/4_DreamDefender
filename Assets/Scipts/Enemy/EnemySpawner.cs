using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawn, maxSpawn;

    private float despawnDistance;

    public List<GameObject> spawnedEnemies = new List<GameObject>();//�б����Ѿ����ɵĹ���

    public int checkPerFrame;//ÿһ֡���Ĺ�������
    private int enemyToCheck;

    public List<WaveInfo> waves;

    public int currentWave;
    private float waveCounter;

    public bool canGoNextWave = true;

    private int animCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        //spawnCounter = timeToSpawn;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;  //������� ����ɾ�������Զ�Ĺ���

        currentWave = -1;
        GoToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthController.instance.gameObject.activeSelf)
        {
            if (currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                UIController.instance.UpdateTimer(waveCounter);

                if (waveCounter <= 0)
                {

                    for (int i = 0; i < spawnedEnemies.Count;i++)
                    {
                        Destroy(spawnedEnemies[i]);
                    }
                    UIController.instance.timeText.text = null;
                    
                    if(animCounter == 1)
                    {
                        animCounter--;
                        canGoNextWave = false;
                        UIController.instance.treasureAnimation.SetActive(true);
                    }


                    if (canGoNextWave)
                        GoToNextWave();

                }

                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;

                    //����Ȩ���������һ����
                    /*
                    int selected = Random.Range(0,waves[currentWave].enemyToSpawn.Count);


                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn[selected], SelectSpawnPoint(), Quaternion.identity);

                    spawnedEnemies.Add(newEnemy);
                    */
                    int totalWeight = 0;

                    foreach(var weight in waves[currentWave].spawnWeight)
                    {
                        totalWeight += weight;
                    }

                    int selected = Random.Range(0,totalWeight);
                    int currentweight = 0; 
                    int i; //��ѡ�еĹ�����

                    for(i = 0;i < waves[currentWave].enemyToSpawn.Count;i++)
                    {
                        currentweight += waves[currentWave].spawnWeight[i];
                        if(selected < currentweight)
                        {
                            break;
                        }
                    }

                    if(i < waves[currentWave].enemyToSpawn.Count)
                    {
                        GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn[i], SelectSpawnPoint(), Quaternion.identity);

                        spawnedEnemies.Add(newEnemy);
                    }
                }
            }
        }

        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget)  //ɾ�������Զ�Ĺ���
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);

                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }

    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;

        if (spawnVerticalEdge) //50%���ҳ���
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else//50%���³���
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }


        return spawnPoint;
    }

    public void GoToNextWave()
    {
        currentWave++;
        animCounter = 1;

        if(currentWave + 2 > waves.Count)
        {
            UIController.instance.waveReportText.text = "���һ�����ƣ���";
        }
        else
        { 
            UIController.instance.waveReportText.text = "��" + (currentWave + 1) + "��" + "  ��ʼ";
        }


        UIController.instance.waveReport.SetActive(true);

        StartCoroutine(TextOut(Color.red, new Color(255, 0, 0, 0), 1f));

        /*
        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1; //����ѭ�����һ��

        }
        */

        waveCounter = waves[currentWave].waveLength; //ͬ����һ����ʱ��
        spawnCounter = waves[currentWave].timeBetweenSpawns; //ͬ����һ���ֵ����ɼ��

    }

    IEnumerator TextOut(Color startColor, Color endColor, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            UIController.instance.waveReportText.color = Color.Lerp(endColor, startColor, (Time.time - startTime) / duration);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            UIController.instance.waveReportText.color = Color.Lerp(startColor, endColor, (Time.time - startTime) / duration);
            yield return null;
        }

        UIController.instance.waveReport.SetActive(false);
        yield break;
    }
}


[System.Serializable]
public class WaveInfo
{
    public List<GameObject> enemyToSpawn = new List<GameObject>();
    public List<int> spawnWeight = new List<int>(); 
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
