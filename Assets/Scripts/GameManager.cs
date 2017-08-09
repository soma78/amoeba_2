using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField]
    public float m_Power {get; set; }

    public const float MAX_PAIN = 13f;
    public const float MIN_PAIN = 4f;

    public int m_numOfHearts;
    public int m_currentPimple;

    public GameObject m_pimplePrefab;
    public static GameManager Instance;
    public Slider m_painSlider;
    public float m_painValue;
    public float m_currentPain;
    public GameObject m_firstHand;
    public GameObject m_secondHand;
    public SpriteRenderer m_faceSprite;
    public bool canDecreaseHp;
    public Sprite[] m_faceImgaeArray;


    public GameObject m_GameOverPanel;
    public GameObject m_WinPanel;

    public Text m_painText;
    //public Text m_touchCount;

    private int m_numofGem;
    private bool canSetRandomPainValue = true;
    private bool canStart = true;
    

    void Awake()
    {
        Instance = this;
        Screen.SetResolution(720, 1280, true);
    }

    void Start()
    {
        m_GameOverPanel.SetActive(false);
        m_WinPanel.SetActive(false);
        m_Power = 0;
        m_numofGem = 7;
        m_currentPimple = m_numofGem;
        m_numOfHearts = 3;

        m_firstHand = GameObject.Find("first_Hand");
        m_secondHand = GameObject.Find("second_Hand");
        m_faceSprite = GameObject.Find("Face").GetComponent<SpriteRenderer>();
        m_painText = GameObject.Find("Pain").GetComponent<Text>();
        
        m_faceImgaeArray = Resources.LoadAll<Sprite>("Face/");
        m_faceSprite.sprite = m_faceImgaeArray[0];

        SetSpriteOnOff(m_firstHand, false);
        SetSpriteOnOff(m_secondHand, false);

        m_painSlider = GameObject.Find("PainBar").GetComponent<Slider>();
        LoadFace();
        CreatePimple();
    }

    void SetSpriteOnOff(GameObject obj, bool value)
    {
        obj.GetComponent<SpriteRenderer>().enabled = value;
    }

    void LoadFace()
    {

    }

    void CreatePimple()
    {
        Vector3 center = new Vector3(0, 0.24f, 0);
        for (int i = 0; i < m_numofGem; i++)
        {
            float radius = Random.Range(0.1f, 2.2f);
            Vector3 pos = RandomCircle(center, radius);
            Instantiate(m_pimplePrefab, pos, Quaternion.identity);
        }
    }

    public void CheckCurrentPimple()
    {
        m_currentPimple--;
        if(m_currentPimple <= 0)
        {
            m_WinPanel.SetActive(true);
        }
    }

    //radius는 반지름임.

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    public void SetPower(float _value)
    {
        m_Power = _value;
    }

    void SetPainRandomValue()
    {
        m_painValue = Random.Range(MIN_PAIN, MAX_PAIN);
        m_painText.text = "#Pain :" + m_painValue.ToString("0");
    }

    void Update()
    {
        
        if(Input.touchCount == 0)
        {
            ResetHand();
        }
        if(Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                canStart = true;
                canDecreaseHp = true;
            }
            if (canStart)
            {
                if (canSetRandomPainValue)
                {
                    SetPainRandomValue();
                    canSetRandomPainValue = false;
                    int randFaceNumber = Random.Range(0, m_faceImgaeArray.Length);
                    m_faceSprite.sprite = m_faceImgaeArray[randFaceNumber];
                }

                m_currentPain += Time.deltaTime * 2f;
                Debug.Log("m_currentPain: " + m_currentPain.ToString());
                //Debug.Log(m_painValue.ToString());
                m_painSlider.value = m_currentPain / m_painValue;
                CheckPain();         
                SetSpriteOnOff(m_firstHand, true);
                SetSpriteOnOff(m_secondHand, true);
                Vector3 firstPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, -1f));
                m_firstHand.transform.position = new Vector3(firstPos.x, firstPos.y, -1f);

                Vector3 secondPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, -1f));
                m_secondHand.transform.position = new Vector3(secondPos.x, secondPos.y, -1f);
            }
              if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    ResetHand();
                    canStart = true;
                    canDecreaseHp = false;
                }
        }
    }

    public void ResetHand()
    {
        m_faceSprite.sprite = m_faceImgaeArray[0];
        m_currentPain = 0;
        m_painSlider.value = m_currentPain / m_painValue;
        SetSpriteOnOff(m_firstHand, false);
        SetSpriteOnOff(m_secondHand, false);
        canSetRandomPainValue = true;
        canStart = false;
    }

    void CheckPain()
    {
        if(m_currentPain >= m_painValue)
        {
            m_numOfHearts--;
            if (m_numOfHearts <= 0) ShowGameOver();
            GameObject.Find("HeartsPanel").GetComponent<HeartContoroller>().ReduceHeart(m_numOfHearts);
            ResetHand();
        }
    }

    void ShowGameOver()
    {
        m_GameOverPanel.SetActive(true);
    }

    public void SOS()
    {
        ResetHand();
        canStart = true;
        canDecreaseHp = false;
    }
}
