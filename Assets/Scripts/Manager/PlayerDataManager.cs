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
        //해시테이블 형태로 레지스트리 영역에 저장
        //실제로 간단하고 가능한 것들은 로컬로 저장함
        //Set 자료형 세개뿐인 것이 단점이지만 요즘에는 데이터를 보통 string으로 저장함 -> JSON 사용

        //PlayerPrefs.DeleteAll();    //test
        var json = JsonUtility.ToJson(m_myData);    //JsonWriter.Serialize(m_myData);
        PlayerPrefs.SetString("PLAYER_DATA", json);
        PlayerPrefs.Save();
    }
}
