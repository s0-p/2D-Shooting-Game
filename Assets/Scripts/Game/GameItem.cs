using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    Camera m_camera;
    SpriteRenderer m_sprRenderer;    
    TweenRotation m_rotTween;
    [SerializeField]
    AnimationCurve m_curveOffsetY = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField]
    AnimationCurve m_curveOffsetX = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    GameItemManager.ItemType m_type;
    [SerializeField]
    float m_duration = 1f;
    [SerializeField]
    Vector2 m_from;
    [SerializeField]
    Vector2 m_to;
    float m_height = 1.5f;
    float m_endPosY = -6f;
    void Awake()
    {
        m_camera = Camera.main;
        m_sprRenderer = GetComponent<SpriteRenderer>();
        m_rotTween = GetComponent<TweenRotation>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameItemManager.Instance.RemoveItem(this);
        switch (m_type)
        {
            case GameItemManager.ItemType.Coin:
                SoundManager.Instance.PlaySFX(SoundManager.AudioClipSFX.get_coin);
                GameUiManager.Instance.SetCoinCount(1);
                break;
            case GameItemManager.ItemType.Gem_Red:
            case GameItemManager.ItemType.Gem_Green:
            case GameItemManager.ItemType.Gem_Blue:
                SoundManager.Instance.PlaySFX(SoundManager.AudioClipSFX.get_gem);
                GameUiManager.Instance.SetCoinCount((int)m_type * 10);
                break;
            case GameItemManager.ItemType.Invincible:
                SoundManager.Instance.PlaySFX(SoundManager.AudioClipSFX.get_invincible);
                break;
            case GameItemManager.ItemType.Magnet:
                SoundManager.Instance.PlaySFX(SoundManager.AudioClipSFX.get_item);
                break;
            default:
                break;
        }
    }
    public void SetItem(Vector3 pos, GameItemManager.ItemType type, Sprite icon)
    {
        transform.position = pos;
        m_type = type;
        m_sprRenderer.sprite = icon;
        StartCoroutine(Coroutine_MoveProcess());

    }
    IEnumerator Coroutine_MoveProcess()
    {
        float time = 0f;
        float valueY = 0f;
        float valueX = 0f;
        m_from = transform.position;
        var dirX = 1 - Random.Range(0, 3);
        m_to = new Vector2(m_from.x, m_endPosY) + (Vector2.right * dirX) * 1.5f;
        transform.localRotation = Quaternion.identity;
        if (m_type >= GameItemManager.ItemType.Gem_Red && m_type <= GameItemManager.ItemType.Gem_Blue)
        {
            m_rotTween.enabled = true;
            m_rotTween.ResetToBeginning();
            m_rotTween.PlayForward();
        }
        else
        {
            m_rotTween.enabled = false;
        }
        while (true)
        {
            if (time > 1f)
            {
                GameItemManager.Instance.RemoveItem(this);
                yield break;
            }
            valueY = m_curveOffsetY.Evaluate(time);
            valueX = m_curveOffsetX.Evaluate(time);
            transform.position = (m_from * (1f - valueX) + m_to * valueX) + Vector2.up * (valueY * m_height);     //??
            var viewPos = m_camera.WorldToViewportPoint(transform.position);
            if (viewPos.x < 0.01f)
            {
                viewPos.x = 0.01f;
                transform.position = m_camera.WorldToViewportPoint(viewPos);
            }
            else if (viewPos.x > 0.95f)
            {
                viewPos.x = 0.95f;
                transform.position = m_camera.WorldToViewportPoint(viewPos);
            }
            time += Time.deltaTime / m_duration;
            yield return null;
        }
    }
    
}
