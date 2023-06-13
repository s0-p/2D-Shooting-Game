using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_OkCancel : MonoBehaviour
{
    [SerializeField]
    UILabel m_titleLabel;
    [SerializeField]
    UILabel m_bodyLabel;
    [SerializeField]
    UILabel m_okBtnLabel;
    [SerializeField]
    UILabel m_cancelBtnLabel;
    [SerializeField]
    Action m_okBtnDel;
    [SerializeField]
    Action m_cancelBtnDel;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SetUI(string title, string body, Action okDel, Action cancelDel, string okBtnText = "OK", string cancelBtnText = "Cancel")
    {
        m_titleLabel.text = title;
        m_bodyLabel.text = body;
        m_okBtnDel = okDel;
        m_cancelBtnDel = cancelDel;
        m_okBtnLabel.text = okBtnText;
        m_cancelBtnLabel.text = cancelBtnText;
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
    public void OnPressCancel()
    {
        if (m_cancelBtnDel != null)
        {
            m_cancelBtnDel();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
        }
    }
}
