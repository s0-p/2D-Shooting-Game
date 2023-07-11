using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : SingletonMonoBehaviour<BuffManager>
{
    [SerializeField]
    PlayerController m_player;
    [SerializeField]
    BgController m_bgCtrl;
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
    public bool GetBuff(BuffType type)
    {
        if (m_buffList.ContainsKey(type))
        {
            return true;
        }
        return false;
    }
    IEnumerator Coroutine_BuffProcess(BuffType type)
    {
        switch (type)
        {
            case BuffType.Invincible:
                m_player.SetInvincibleEffect(true);
                m_bgCtrl.SetSpeed(6f);
                CameraShake.Instance.Shake(m_buffList[type].Data.Duration, 0.05f);
                MonsterManager.Instance.ResetCreateMonsters(6f);
                m_player.SetShockwaveEffect(false);
                break;
            case BuffType.Magnet:
                m_player.SetMagnetEffect(true);
                break;
            //case BuffType.DualShot:
            //    break;
        }
        while (true)
        {
            var buff = m_buffList[type];
            buff.Time += Time.deltaTime;
            m_buffList[type] = buff;
            if (buff.Time >= buff.Data.Duration)
            {
                switch (type)
                {
                    case BuffType.Invincible:
                        m_player.SetInvincibleEffect(false);
                        m_player.SetShockwaveEffect(true);
                        m_bgCtrl.SetSpeed(1f);
                        MonsterManager.Instance.ResetCreateMonsters(1f);
                        break;
                    case BuffType.Magnet:
                        m_player.SetMagnetEffect(false);
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
