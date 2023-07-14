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
    // Start is called before the first frame update
    protected override void OnStart()
    {
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
}
