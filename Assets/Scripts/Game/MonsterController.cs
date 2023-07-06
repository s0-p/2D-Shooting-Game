using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    Animator m_animator;
    [SerializeField]
    float m_speed = 2f;
    public int Hp { get; set; }
    public MonsterType Type { get; set; }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * m_speed * Time.deltaTime;
    }
    void OnEnable()
    {
        Hp = (int)((int)(Type + 1) * 1.5f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collider_Bottom"))
        {
            MonsterManager.Instance.RemoveMonster(this);
        }
    }
    public void SetDamage(int damage)
    {
        Hp -= damage;
        m_animator.Play("Hit", 0, 0f);
        if (Hp <= 0)
        {
            Hp = 0;
            if (Type == MonsterType.Bomb)
            {
                MonsterManager.Instance.BombMonsters(transform.position.y);
            }
            else
            {
                MonsterManager.Instance.RemoveMonster(this);
                SetDie();
            }
        }
    }
    public void SetDie()
    {
        EffectPool.Instance.CreateEffect(transform.position);
        GameItemManager.Instance.CreateItem(transform.position);
        GameUiManager.Instance.SetHuntScore((int)Mathf.Pow(2, 6 + (int)Type));
    }
}
