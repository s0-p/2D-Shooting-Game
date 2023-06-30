using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxController : MonoBehaviour
{
    ParticleSystem[] m_particles;
    // Start is called before the first frame update
    void Start()
    {
        m_particles = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isPlaying = false;
        for (int i = 0; i < m_particles.Length; i++)
        {
            ParticleSystem particle = m_particles[i];
            if (particle.isPlaying)
            {
                isPlaying = true;
                break;
            }
        }
        if (!isPlaying)
        {
            EffectPool.Instance.RemoveEffect(this);
        }
    }
}
