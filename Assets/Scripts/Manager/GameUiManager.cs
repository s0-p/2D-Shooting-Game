using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameUiManager : SingletonMonoBehaviour<GameUiManager>
{
    [SerializeField]
    UILabel m_flightScoreLabel;
    [SerializeField]
    UILabel m_huntScoreLabel;
    [SerializeField]
    UILabel m_coinCountLabel;
    StringBuilder m_sb = new StringBuilder();

    [SerializeField]
    bool m_isPause;
    int m_flightScore;
    int m_huntScore;
    int m_coinCount;

    public int FlightScore { get { return m_flightScore; } }
    public int HuntScore { get { return m_huntScore; } }
    public int CoinCount { get { return m_coinCount; } }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_flightScore = 0;
        SetFlightScore(0);
        m_huntScore = 0;
        SetHuntScore(0);
        m_coinCount = 0;
        SetCoinCount(0);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void SetFlightScore(float dist)
    {
        m_flightScore = Mathf.CeilToInt(dist * 1000f);
        m_sb.AppendFormat("{0:n0}M", m_flightScore);
        m_flightScoreLabel.text = m_sb.ToString();
        m_sb.Clear();
    }
    public void SetHuntScore(int deadMon)
    {
        m_huntScore += deadMon;
        m_sb.AppendFormat("{0:n0}", m_huntScore);
        m_huntScoreLabel.text = m_sb.ToString();
        m_sb.Clear();
    }
    public void SetCoinCount(int coin)
    {
        m_coinCount += coin;
        m_sb.AppendFormat("{0:n0}", m_coinCount);
        m_coinCountLabel.text = m_sb.ToString();
        m_sb.Clear();
    }
    public void SetPause()
    {
        m_isPause = m_isPause ? false : true;   //false로 시작해 버튼을 누를때마다 값이 바뀜
        Time.timeScale = m_isPause ? 0 : 1;
    }
}
