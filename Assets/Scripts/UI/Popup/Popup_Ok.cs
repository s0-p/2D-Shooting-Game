using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Popup_Ok : MonoBehaviour
{
    [SerializeField]
    UILabel m_titleLabel;
    [SerializeField]
    UILabel m_bodyLabel;
    [SerializeField]
    UILabel m_okBtnLabel;
    [SerializeField]
    Action m_okBtnDel;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SetUI(string title, string body, Action okDel, string okBtnText = "OK")
    {
        m_titleLabel.text = title;
        m_bodyLabel.text = body;
        m_okBtnDel = okDel;
        m_okBtnLabel.text = okBtnText;
    }
    public void OnPressOK()
    {
        if (m_okBtnDel != null)
        {
            m_okBtnDel();
            //m_okBtnDel.Invoke();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
        }
    }
}
