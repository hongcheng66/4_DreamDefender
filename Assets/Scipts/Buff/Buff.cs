using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public BuffStats buffstats;
}

[System.Serializable]
public class BuffStats
{
    public string name; //Buff����
    public Sprite icon; //Buffͼ��
    public int level; //Buff�ȼ�
    public string describeText; //���� 
}
