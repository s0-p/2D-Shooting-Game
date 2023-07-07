using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public enum AudioType
    {
        BGM,
        SFX,
        Max
    }
    public enum AudioClipBGM
    {
        dragon_flight,
        Max
    }
    public enum AudioClipSFX
    {
        get_coin,
        get_gem,
        get_invincible,
        get_item,
        mon_die,
        Max
    }

    AudioSource[] m_audio;
    [SerializeField]
    AudioClip[] m_audioClipBGM;
    [SerializeField]
    AudioClip[] m_audioClipSFX;
    const int maxVolumeLevel = 5;
    const int MaxPlayCount = 3;
    Dictionary<AudioClipSFX, int> m_sfxPlayCountList = new Dictionary<AudioClipSFX, int>();
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_audio = new AudioSource[(int)AudioType.Max];

        m_audio[(int)AudioType.BGM] = gameObject.AddComponent<AudioSource>();
        m_audio[(int)AudioType.BGM].loop = true;
        m_audio[(int)AudioType.BGM].playOnAwake = false;
        m_audio[(int)AudioType.BGM].rolloffMode = AudioRolloffMode.Linear;

        m_audio[(int)AudioType.SFX] = gameObject.AddComponent<AudioSource>();
        m_audio[(int)AudioType.SFX].loop = false;
        m_audio[(int)AudioType.SFX].playOnAwake = false;
        m_audio[(int)AudioType.SFX].rolloffMode = AudioRolloffMode.Linear;

        m_audioClipBGM = Resources.LoadAll<AudioClip>("Sound/BGM");
        m_audioClipSFX = Resources.LoadAll<AudioClip>("Sound/SFX");
        PlayBGM(AudioClipBGM.dragon_flight);
    }
    public void PlayBGM(AudioClipBGM bgm)
    {
        m_audio[(int)AudioType.BGM].clip = m_audioClipBGM[(int)bgm];
        m_audio[(int)AudioType.BGM].Play();
    }
    public void PlaySFX(AudioClipSFX sfx)
    {
        int count = 0;
        if (m_sfxPlayCountList.TryGetValue(sfx, out count))
        {
            if (count >= MaxPlayCount) return;
            m_sfxPlayCountList[sfx]++;
        }
        else
        {
            m_sfxPlayCountList.Add(sfx, 1);
        }
        m_audio[(int)AudioType.SFX].PlayOneShot(m_audioClipSFX[(int)sfx]);
        StartCoroutine(Coroutine_CheckPlayCount(sfx, m_audioClipSFX[(int)sfx].length));
    }
    IEnumerator Coroutine_CheckPlayCount(AudioClipSFX sfx, float duration)
    {
        yield return new WaitForSeconds(duration);
        m_sfxPlayCountList[sfx]--;
        if (m_sfxPlayCountList[sfx] <= 0)
        {
            m_sfxPlayCountList.Remove(sfx);
        }
    }
    public void SetVolumeBGM(int level)
    {
        if (level < 0) level = 0;
        if (level > maxVolumeLevel) level = maxVolumeLevel;
        m_audio[(int)AudioType.BGM].volume = (float)level / maxVolumeLevel;
    }
    public void SetVolumeSFX(int level)
    {
        if (level < 0) level = 0;
        if (level > maxVolumeLevel) level = maxVolumeLevel;
        m_audio[(int)AudioType.SFX].volume = (float)level / maxVolumeLevel;
    }
    public void SetVolume(int level)
    {
        SetVolumeBGM(level);
        SetVolumeSFX(level);
    }
    public void SetMute(bool isOn)
    {
        m_audio[(int)AudioType.BGM].mute = isOn;
        m_audio[(int)AudioType.SFX].mute = isOn;
    }
}
