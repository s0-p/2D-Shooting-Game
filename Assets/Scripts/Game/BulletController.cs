using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 12f;
    [SerializeField]
    PlayerController m_player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * m_speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collider_Top"))
        {
            m_player.RemoveBullet(this);
        }
    }
    public void InitBullet(PlayerController player)
    {
        m_player = player;
    }
}
