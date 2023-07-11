using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    None = -1,
    Invincible,
    Magnet,
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
public struct BuffInfo  //���� �������� ����
{
    public BuffData Data;
    public float Time;
}