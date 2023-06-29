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
    [SerializeField]
    GameObject[] m_monPrefabs;
    Dictionary<MonsterType, GameObjectPool<MonsterController>> m_monsterPool = new Dictionary<MonsterType, GameObjectPool<MonsterController>>();
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
    
    // Update is called once per frame
    void Update()
    {
        
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
            var mon = m_monsterPool[type].Get();
            mon.transform.position = m_startPos + Vector2.right * posGap * i;
            mon.gameObject.SetActive(true);
        }
    }
    public void RemoveMonster(MonsterController monster)
    {
        monster.gameObject.SetActive(false);
        m_monsterPool[monster.Type].Set(monster);
    }
}
