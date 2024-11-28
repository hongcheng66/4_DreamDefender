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
    public string name; //Buff名字
    public Sprite icon; //Buff图标
    public int level; //Buff等级
    public string describeText; //描述 
}
