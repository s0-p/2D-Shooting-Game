using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    None = -1,
    Magnet,
    Invincible,
    DualShot,
    Max
}
[Serializable]
public struct BuffData
{
    public BuffType Type;
    public float Duration;
}
[Serializable]
public struct BuffInfo  //현재 상태정보 저장
{
    public BuffData Data;
    public float Time;
}