using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : SingletonMonoBehaviour<BuffManager>
{
    [SerializeField]
    PlayerController m_player;
    [SerializeField]
    List<BuffData> m_buffDataList;
    Dictionary<BuffType, BuffData> m_buffTable = new Dictionary<BuffType, BuffData>();
    Dictionary<BuffType, BuffInfo> m_buffList = new Dictionary<BuffType, BuffInfo>();
    // Start is called before the first frame update
    protected override void OnStart()
    {
        for (int i = 0; i < m_buffDataList.Count; i++)
        {
            m_buffTable.Add((BuffType)i, m_buffDataList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBuff(BuffType type)
    {
        if (m_buffList.ContainsKey(type))
        {
            var buff = m_buffList[type];
            buff.Time = 0f;
            m_buffList[type] = buff;
        }
        else
        {
            BuffInfo buff = new BuffInfo() { Data = m_buffTable[type], Time = 0f };
            m_buffList.Add(type, buff);
            StartCoroutine(Coroutine_BuffProcess(type));
        }
    }
    IEnumerator Coroutine_BuffProcess(BuffType type)
    {
        switch (type)
        {
            case BuffType.Magnet:
                m_player.SetMagnetEffect(true);
                break;
            case BuffType.Invincible:
                break;
            //case BuffType.DualShot:
            //    break;
        }
        while (true)
        {
            var buff = m_buffList[type];
            buff.Time += Time.deltaTime;
            m_buffList[type] = buff;
            if (buff.Time > buff.Data.Duration)
            {
                switch (type)
                {
                    case BuffType.Magnet:
                        m_player.SetMagnetEffect(false);
                        break;
                    case BuffType.Invincible:
                        break;
                    //case BuffType.DualShot:
                    //    break;
                }
                m_buffList.Remove(type);
                yield break;
            }
            yield return null;
        }
    }
}
