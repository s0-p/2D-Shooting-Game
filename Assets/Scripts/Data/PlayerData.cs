using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HeroData
{
    public bool isOpen;
    public string Name;
    public string Class;
    public int Level;
    public int Price;
}
[System.Serializable]
public struct PlayerData
{
    public static int BasicGold = 1000;
    public static int BasicGem = 100;
    public int BestScore;
    public int GoldOwned;
    public int GemOwned;
    public int HeroIndex;
    public HeroData[] HeroData;

    public PlayerData(HeroData[] heroDatas)     //구조체 생성자는 매개변수 필수
    {
        BestScore = 0;
        GoldOwned = BasicGold;
        GemOwned = BasicGem;
        HeroIndex = 0;
        HeroData = heroDatas;
    }
}
