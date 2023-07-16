using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonFx.Json;

public class PlayerDataManager : SingletonDontDestroy<PlayerDataManager>
{
    [SerializeField]
    HeroData[] m_heroDatas;
    [SerializeField]
    PlayerData m_myData;
    
    public int BestScore { get { return m_myData.BestScore; } set { m_myData.BestScore = value; } }
    public int HeroIndex { get { return m_myData.HeroIndex; } set { m_myData.HeroIndex = value; } }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        //PlayerPrefs.DeleteAll();    //test
        if (!Load())
        {
            m_myData = new PlayerData(m_heroDatas);
            Save();
        }
    }
    public bool Load()
    {
        if (!PlayerPrefs.HasKey("PLAYER_DATA"))  return false;

        var json = PlayerPrefs.GetString("PLAYER_DATA", null);
        if (json == null) return false;

        print(json);
        m_myData = JsonUtility.FromJson<PlayerData>(json);  //JsonReader.Deserialize<PlayerData>(json);
        return true;
        
    }
    public void Save()
    {
        //PlayerPrefs
        //�ؽ����̺� ���·� ������Ʈ�� ������ ����
        //������ �����ϰ� ������ �͵��� ���÷� ������
        //Set �ڷ��� �������� ���� ���������� ���򿡴� �����͸� ���� string���� ������ -> JSON ���

        //PlayerPrefs.DeleteAll();    //test
        var json = JsonUtility.ToJson(m_myData);    //JsonWriter.Serialize(m_myData);
        PlayerPrefs.SetString("PLAYER_DATA", json);
        PlayerPrefs.Save();
    }
    public void IncreaseGold(int gold)
    {
        m_myData.GoldOwned += gold;
        Save();
    }
    public bool DecreaseGold(int gold)
    {
        if (m_myData.GoldOwned >= gold)
        {
            m_myData.GoldOwned -= gold;
            return true;
        }
        return false;
    }
}
