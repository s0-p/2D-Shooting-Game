using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 0.2f;
    SpriteRenderer m_sprRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_sprRenderer = GetComponent<SpriteRenderer>();
        SoundManager.Instance.PlayBGM(SoundManager.AudioClipBGM.dragon_flight);
    }

    // Update is called once per frame
    void Update()
    {
        m_sprRenderer.material.mainTextureOffset += Vector2.up * m_speed* Time.deltaTime;
        GameUiManager.Instance.SetFlightScore(m_sprRenderer.material.mainTextureOffset.y);
    }
}
