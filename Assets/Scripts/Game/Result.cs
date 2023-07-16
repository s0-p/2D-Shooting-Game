using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Result : SingletonMonoBehaviour<Result>
{
    [SerializeField]
    UI2DSprite m_sdSprite;
    [SerializeField]
    GameObject m_bestObj;
    [SerializeField]
    UILabel m_totalScoreLabel;
    [SerializeField]
    UILabel m_distScoreLabel;
    [SerializeField]
    UILabel m_huntScoreLabel;
    [SerializeField]
    UILabel m_goldOwnedLabel;
    [SerializeField]
    UILabel m_bestScoreLabel;

    StringBuilder m_sb = new StringBuilder();
    // Start is called before the first frame update
    protected override void OnStart()
    {
        Hide();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        bool isBest = false;
        gameObject.SetActive(true);
        m_bestObj.SetActive(false);
        int totalScore = GameUiManager.Instance.FlightScore + GameUiManager.Instance.HuntScore;
        m_sb.AppendFormat("{0:n0}", totalScore);
        m_totalScoreLabel.text = m_sb.ToString();
        m_sb.Clear();

        m_sb.AppendFormat("{0:n0}", GameUiManager.Instance.FlightScore);
        m_distScoreLabel.text = m_sb.ToString();
        m_sb.Clear();

        m_sb.AppendFormat("{0:n0}", GameUiManager.Instance.HuntScore);
        m_huntScoreLabel.text = m_sb.ToString();
        m_sb.Clear();

        m_sb.AppendFormat("{0:n0}", GameUiManager.Instance.CoinCount);
        m_goldOwnedLabel.text = m_sb.ToString();
        m_sb.Clear();

        if (totalScore > PlayerDataManager.Instance.BestScore)
        {
            isBest = true;
            m_bestObj.SetActive(true);
            PlayerDataManager.Instance.BestScore = totalScore;
            PlayerDataManager.Instance.Save();
        }
        m_sb.AppendFormat("{0:n0}", PlayerDataManager.Instance.BestScore);
        m_bestScoreLabel.text = m_sb.ToString();
        m_sb.Clear();

        m_sb.AppendFormat("SD/sd_{0:00}{1}", PlayerDataManager.Instance.HeroIndex + 1, isBest ? "_highscore" : "");
        m_sdSprite.sprite2D = Resources.Load<Sprite>(m_sb.ToString());
        m_sb.Clear();

        PlayerDataManager.Instance.IncreaseGold(GameUiManager.Instance.CoinCount);
    }
    public void SetUI()
    {
        Show();
    }
    public void OnPressConfirm()
    {
        LoadSceneManager.Instance.LoadSceneAsync(Scene.Lobby);
    }
}
