using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : SingletonMonoBehaviour<CameraShake>
{
    [SerializeField]
    float m_duration = 1f;
    [SerializeField]
    float m_power = 0.1f;
    Vector3 m_orgPos;
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_orgPos = transform.position;
    }
    
    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(Coroutine_RandomPosition());
    }
    public void Shake(float duration, float power)
    {
        m_duration = duration;
        m_power = power;
        Shake();
    }
    IEnumerator Coroutine_RandomPosition()
    {
        float time = 0f;
        Vector3 pos = Vector3.zero;
        while (true)
        {
            if (time > m_duration)
            {
                transform.position = m_orgPos;
                yield break;
            }
            pos = Random.insideUnitCircle.normalized * m_power;
            transform.position = new Vector3(pos.x, pos.y, m_orgPos.z);
            time += Time.deltaTime;
            yield return null;
        }
    }

}
