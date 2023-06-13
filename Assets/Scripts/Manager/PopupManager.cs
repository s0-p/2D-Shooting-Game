using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : SingletonDontDestroy<PopupManager>
{
    [SerializeField]
    GameObject m_popupOkCancelPrefab;
    [SerializeField]
    GameObject m_popupOkPrefab;
    const int popupDepth = 1000;
    const int popupGap = 10;
    List<GameObject> m_popupList = new List<GameObject>();
    public bool IsOpen { get { return m_popupList.Count > 0; } }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_popupOkCancelPrefab = Resources.Load<GameObject>("Prefab/UI/Popup/PopupOkCancel");
        m_popupOkPrefab = Resources.Load<GameObject>("Prefab/UI/Popup/PopupOk");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (UnityEngine.Random.Range(0, 100) % 2 == 0)
                Open_PopupOkCancel("공지사항", "오늘 수업은 보강입니다.", null, null, "수강하기", "종강하기");
            else
                Open_PopupOk("안내", "내일은 주말이라 수업이 없습니다.", null, "확인");
        }
    }
    public void Open_PopupOk(string title, string body, Action okDel, string okBtnText = "OK")
    {
        var obj = Instantiate(m_popupOkPrefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;

        var panels = obj.GetComponentsInChildren<UIPanel>();
        for (int i = 0; i < panels.Length; i++)
        {
            var panel = panels[i];
            panel.depth = popupDepth + m_popupList.Count * popupGap + i;
        }

        var popup = obj.GetComponent<Popup_Ok>();
        popup.SetUI(title, body, okDel, okBtnText);
        m_popupList.Add(obj);
    }
    public void Open_PopupOkCancel(string title, string body, Action okDel, Action cancelDel, string okBtnText = "OK", string cancelBtnText = "Cancel")
    {
        var obj = Instantiate(m_popupOkCancelPrefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;

        var panels = obj.GetComponentsInChildren<UIPanel>();
        for (int i = 0; i < panels.Length; i++)
        {
            var panel = panels[i];
            panel.depth = popupDepth + m_popupList.Count * popupGap + i;
        }

        var popup = obj.GetComponent<Popup_OkCancel>();
        popup.SetUI(title, body, okDel, cancelDel, okBtnText, cancelBtnText);
        m_popupList.Add(obj);
    }
    public void ClosePopup()
    {
        if (m_popupList.Count == 0) return;
        Destroy(m_popupList[m_popupList.Count - 1]);
        m_popupList.RemoveAt(m_popupList.Count - 1);
    }
}
