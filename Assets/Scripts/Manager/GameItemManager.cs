using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemManager : SingletonMonoBehaviour<GameItemManager>
{
    //[Header("")]
    //[Tooltip("")]
    //[Space()]
    //[Range(,)]
    public enum ItemType
    {
        Coin,
        Gem_Red,
        Gem_Green,
        Gem_Blue,
        Invincible,
        Magnet,
        Max
    }

    [SerializeField]
    GameObject m_itemPrefab;
    GameObjectPool<GameItem> m_itemPool;
    [SerializeField]
    Sprite[] m_icons;
    float[] m_itemTable = { 96.0f, 1.5f, 0.5f, 0.3f, 0.2f, 2f };
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_itemPool = new GameObjectPool<GameItem>(5, () =>
        {
            var obj = Instantiate(m_itemPrefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var item = obj.GetComponent<GameItem>();
            return item;
        });
    }
    public void CreateItem(Vector3 pos)
    {
        var item = m_itemPool.Get();
        item.gameObject.SetActive(true);
        var type = (ItemType)Util.GetPriority(m_itemTable);
        item.SetItem(pos,type, m_icons[(int)type]);
    }
    public void RemoveItem(GameItem item)
    {
        item.gameObject.SetActive(false);
        m_itemPool.Set(item);
    }
}
