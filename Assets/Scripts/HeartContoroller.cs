using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HeartContoroller : MonoBehaviour {

    public Image[] m_heartArray;
   
    public Sprite m_OnHeart;
    public Sprite m_OffHeart;

    void Start()
    {

        m_heartArray = GetComponentsInChildren<Image>();

        for (int i = 0; i < GameManager.Instance.m_numOfHearts-1; i++)
        {
            m_heartArray[i].sprite = m_OnHeart;
        }
       
    }

    public void ReduceHeart(int _remindHeart)
    {

        for (int i = 0; i < m_heartArray.Length; i++)
        {
            m_heartArray[i].sprite = m_OffHeart;
        }

        for (int i = 0; i < _remindHeart; i++)
        {
            m_heartArray[i].sprite = m_OnHeart;
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void GameWin()
    {
        SceneManager.LoadScene(1);
    }

     
}
