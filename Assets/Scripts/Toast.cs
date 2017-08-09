using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour {

    public const float MAX_DISTANCE = 600f;

    public Transform m_midCheckObject;
    public Text m_distanceText;
    public Text m_powerText;

    public float m_power;
    private float m_distance;
    private Vector3 firstPos;
    private Vector3 secondPos;
    private Vector2 midPos;
    
    void Start()
    {
        m_distanceText = GameObject.Find("Distance").GetComponent<Text>();
        m_powerText = GameObject.Find("Power").GetComponent<Text>();
        UpdateText();
    }

	void Update()
    {
        if(Input.touchCount == 2)
        {
            m_distance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            if(m_distance < 400f)
            {
                CalculatePower();
            }
            else
            {
                m_power = 0;
            }
            GameManager.Instance.SetPower(m_power);
            

            UpdateText();
            if(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                firstPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, -1f));

                secondPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, -1f));

                CheckMidPos();
                

            }
        }
    }

    void CalculatePower()
    {
        m_power = Mathf.Pow((MAX_DISTANCE - m_distance), 2)/10000;
    }

    void UpdateText()
    {
        m_distanceText.text = "#_Distance: " + m_distance.ToString("0");
        m_powerText.text = "#_Power: " + m_power.ToString("0.0");
    }
    void CheckMidPos()
    {
        midPos = new Vector2((firstPos.x + secondPos.x) / 2, (firstPos.y + secondPos.y) / 2);
        m_midCheckObject.transform.position = midPos;
    }
}
