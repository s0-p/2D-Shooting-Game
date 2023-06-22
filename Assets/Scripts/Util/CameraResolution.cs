using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraResolution : MonoBehaviour
{
    Camera m_camera;
    [SerializeField]
    float m_fixedWidth;
    [SerializeField]
    float m_fixedHeight;
    float m_fixedAspectRatio;
    float m_currAspectRatio;
    // Start is called before the first frame update
    void Start()
    {
        float w = 0f, h = 0f;
        float x = 0f, y = 0f;
        m_camera = GetComponent<Camera>();
        m_fixedAspectRatio = m_fixedWidth / m_fixedHeight;
        m_currAspectRatio = (float)Screen.width / Screen.height;

        if (m_fixedAspectRatio ==  m_currAspectRatio)
        {
            m_camera.rect = new Rect(0f, 0f, 1f, 1f);
        }
        else if (m_currAspectRatio > m_fixedAspectRatio)    //현재 화면이 가로가 더 김
        {
            w = m_fixedAspectRatio / m_currAspectRatio;
            x = (1 - w) / 2f;
            m_camera.rect = new Rect(x, 0f, w, 1f);
        }
        else if (m_currAspectRatio < m_fixedAspectRatio)    //현재 화면이 세로가 더 김
        {
            h = m_currAspectRatio / m_fixedAspectRatio;
            y = (1 - h) / 2f;
            m_camera.rect = new Rect(0f, y, 1f, h);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
