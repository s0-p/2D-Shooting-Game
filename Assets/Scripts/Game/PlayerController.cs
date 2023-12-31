using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    Transform m_bulletPos;
    GameObjectPool<BulletController> m_bulletPool;
    Camera m_camera;
    RaycastHit2D m_hit;
    [SerializeField]
    float m_speed = 4f;
    Vector3 m_dir;

    [SerializeField]
    Animator m_animator;
    Vector3 m_startPos;
    Vector3 m_prePos;
    bool m_isDrag;

    [Space(10)]
    [Header("���� ȿ��")]
    [SerializeField]
    GameObject m_fxMagnetObj;
    [SerializeField]
    GameObject m_fxInvincibleObj;
    [SerializeField]
    GameObject m_fxShockwaveObj;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        m_animator = GetComponent<Animator>();
        m_bulletPool = new GameObjectPool<BulletController>(5, () =>
        {
            var obj = Instantiate(m_bulletPrefab);
            obj.SetActive(false);
            var bullet = obj.GetComponent<BulletController>();
            bullet.InitBullet(this);
            return bullet;
        });
        SetMagnetEffect(false);
        SetInvincibleEffect(false);
        SetShockwaveEffect(false);
        //InvokeRepeating("CreateBullet", 2f, 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_isDrag = true;
            m_startPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_isDrag = false;
        }
        if (m_isDrag)
        {
            var endPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            var dir = endPos - m_startPos;
            dir.y = 0f;
            m_startPos = endPos;
            m_prePos = transform.position;
            transform.position += dir;

            var dist = transform.position - m_prePos;
            m_hit = Physics2D.Raycast(m_prePos, dist.normalized, Mathf.Abs(dir.x), 1 << LayerMask.NameToLayer("Background"));
            Debug.DrawRay(m_prePos, dist.normalized * Mathf.Abs(dir.x), Color.magenta, 1f);
            if (m_hit.collider != null)
            {
                if (m_hit.collider.CompareTag("Collider_Left") && dir.x < 0f || m_hit.collider.CompareTag("Collider_Right") && dir.x > 0f)
                    transform.position = m_hit.point;
            }
        }
        //m_prePos = transform.position;
        //transform.position += m_dir * m_speed * Time.deltaTime;

        /*var viewPos = m_camera.WorldToViewportPoint(transform.position);
        if (viewPos.x <= 0f || viewPos.x >= 1f)
        {
            if (viewPos.x <= 0f)    viewPos.x = 0f;
            else    viewPos.x = 1f;
            transform.position = m_camera.ViewportToWorldPoint(viewPos);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            gameObject.SetActive(false);
            CancelInvoke("CreateBullet");
            MonsterManager.Instance.CancelCreateMonsters();
            GameUiManager.Instance.Hide();
            Result.Instance.SetUI();
        }
    }
    void CreateBullet()
    {
        var bullet = m_bulletPool.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = m_bulletPos.position;
    }
    public void RemoveBullet(BulletController bullet)
    {
        bullet.gameObject.SetActive(false);
        m_bulletPool.Set(bullet);
    }
    public void SetMagnetEffect(bool isOn)
    {
        m_fxMagnetObj.SetActive(isOn);
    }
    public void SetInvincibleEffect(bool isOn)
    {
        m_fxInvincibleObj.SetActive(isOn);
        print(isOn);
        print(m_fxInvincibleObj.activeSelf);
        if (isOn)
        {
            m_animator.Play("Invincible", 0, 0f);
            CancelInvoke("CreateBullet");
        }
        else
        {
            m_animator.Play("Idle2", 0, 0f);
            InvokeRepeating("CreateBullet", 0.1f, 0.15f);
        }
    }
    public void SetShockwaveEffect(bool isOn)
    {
        m_fxShockwaveObj.SetActive(isOn);
    }
}
