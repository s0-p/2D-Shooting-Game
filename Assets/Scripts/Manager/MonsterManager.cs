using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    White,
    Yellow,
    Pink,
    Bomb,
    Max
}
public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    GameObject[] m_monPrefabs;
    Dictionary<MonsterType, GameObjectPool<MonsterController>> m_monsterPool = new Dictionary<MonsterType, GameObjectPool<MonsterController>>();
    List<MonsterController> m_monList = new List<MonsterController>();
    Vector2 m_startPos = new Vector2(-2.67f, 6f);
    float posGap = 1.329f;
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_monPrefabs = Resources.LoadAll<GameObject>("Prefab/Monster");
        foreach (var prefab in m_monPrefabs)
        {
            MonsterType type = (MonsterType)int.Parse(prefab.name.Split('.')[0]) - 1;
            var pool = new GameObjectPool<MonsterController>(2, () =>
            {
                var obj = Instantiate(prefab);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                var mon = obj.GetComponent<MonsterController>();
                mon.Type = type;
                return mon;
            });
            m_monsterPool.Add(type, pool);
        }
        InvokeRepeating("CreateMonsters", 2f, 3f);
    }
    void CreateMonsters()
    {
        MonsterType type;
        bool isBomb = false;
        bool isTry = false;
        for (int i = 0; i < 5; i++)
        {
            do
            {
                isTry = false;
                type = (MonsterType)Random.Range((int)MonsterType.White, (int)MonsterType.Max);
                if (type == MonsterType.Bomb && !isBomb)
                {
                    isBomb = true;
                    isTry = false;
                }
                else if (type == MonsterType.Bomb && isBomb)
                {
                    isTry = true;
                }
            } while (isTry);
            var monster = m_monsterPool[type].Get();
            monster.transform.position = m_startPos + Vector2.right * posGap * i;
            monster.gameObject.SetActive(true);
            m_monList.Add(monster);
        }
    }
    public void RemoveMonster(MonsterController monster)
    {
        monster.gameObject.SetActive(false);
        if (m_monList.Remove(monster))
            m_monsterPool[monster.Type].Set(monster);
    }
    public void BombMonsters(float bombMonY)
    {
        for (int i = 0; i < m_monList.Count; i++)
        {
            if (m_monList[i].transform.position.y == bombMonY)
            {
                m_monList[i].gameObject.SetActive(false);
                m_monsterPool[m_monList[i].Type].Set(m_monList[i]);
                m_monList[i].SetDie();
            }
        }
        m_monList.RemoveAll(mon => !mon.gameObject.activeSelf);
    }
    
}
