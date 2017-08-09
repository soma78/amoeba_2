using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TouchTest : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 pos2;
    public GameObject finger; //생성하지 말고 원래 있는데 껏다 켜다로 해보자.ㅜㅜ
    public GameObject finger2;
    public GameObject Needle; //중간 값 표현 object
    public Text LeftHandTime, RightHandTime, MidPoint, Score, canGetScore; 
    public float _leftHandTime, _RightHandTime;
    Vector2 _midPoint;
    public GameObject BG;//바탕 색깔 바꿀려고 불러옴
    public Text Distance;
    public float _distance;
    public int _score; // 점수 란
    public bool _canGetScore; //점수 손 때야지 다시 점수 획득 가능하도록 확인하는 flag
   

    void Start()
    {
        BG = GameObject.Find("BG");
        _canGetScore = true; //처음에는 점수 받을수 없음이아니라, 다른 거리 조건도 있으니까 false가 아닌 true로 해준다.
        _score = 0;
        finger = GameObject.Find("finger2");
        Needle = GameObject.Find("needle");
        _leftHandTime =0;
        _RightHandTime = 0;
        _distance = 0;
        finger2 = GameObject.Find("finger1");
        finger.SetActive(false);
        finger2.SetActive(false);
        Distance = GameObject.Find("Distance").GetComponent<Text>();//직접 열결하기 한번 해봤음
        Score = GameObject.Find("Score").GetComponent<Text>();
        canGetScore = GameObject.Find("canGetScore").GetComponent<Text>();
    }

    void Update()
    {
        LeftHandTime.text = ("LeftHandTime :" + _leftHandTime.ToString("0.0")); //왼쪽 시간 표시
        RightHandTime.text = ("RightHandTIme :" + _RightHandTime.ToString("0.0")); //오른쪽 시간 표시
        MidPoint.text = ("MidPoint :" + _midPoint); //중간 좌표 표시
        Score.text = ("Score: $<color=#ff0000>" + _score.ToString() + "</color>");
        canGetScore.text = ("CanGetScore : " + _canGetScore);
        chekcMidPoint();//중요!!!!update 코드가 너무 지저분해지고 나서야, 이 모든걸 함수로 구현할껄 하는 후회가 든다..ㅠㅠ



        if (Input.touchCount > 0)
        {

            _leftHandTime += Time.deltaTime;
            if (Input.touchCount > 1)
            {
                if (Vector2.Distance(pos, pos2) != 0) //두 비교값이 Null이 아닐때..
                {
                    _distance = Vector2.Distance(pos, pos2);
                    Distance.text = ("Distance : " + _distance.ToString("0.00")); // 0.0에 " 표기 안했더니 에러 났따.

                }
                _RightHandTime += Time.deltaTime;//오른손 누르고 있을때 시간 올라감

                BG.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, _RightHandTime * 0.2f); // 얼굴이 빨갛게 변함, Time.time으로 했더니 손때고 난다음에도 리셋이 당연히 안되고 맥스 빨간색으로 되있음

                if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
            
                   
                    pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(1).position.x, Input.GetTouch(1).position.y, 5));

                    finger2.transform.position = new Vector3(pos2.x-0.8f, pos2.y, pos2.z);
                    finger2.SetActive(true);
                }
                if (Input.GetTouch(1).phase == TouchPhase.Ended) //오른손을 땟을때
                    //한손만 때도 양손 다 땐것과 같은 기능 구현으로 버그 수정
                {
                    finger2.gameObject.SetActive(false);
                    _leftHandTime = 0;
                    BG.GetComponent<SpriteRenderer>().color = Color.white; //얼굴 색깔이 원래데로 돌아옴
                    _distance = 0; //점수 획득 불가능하기 위해서 추가
                    _canGetScore = true; //다시 점수 획득 가능 reset canGetScore
                    finger.gameObject.SetActive(false);
                    _RightHandTime = 0;
                    


                }

            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved) //왼손을 땟을때
            {
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 5));

                finger.transform.position = new Vector3(pos.x+0.8f, pos.y, pos.z);
                finger.SetActive(true);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended) 
            {
                finger.gameObject.SetActive(false);
                _RightHandTime = 0;
                _distance = 0; //점수 획득 불가능하기 위해서 추가
 
                finger2.gameObject.SetActive(false);
                _leftHandTime = 0;
                BG.GetComponent<SpriteRenderer>().color = Color.white; //얼굴 색깔이 원래데로 돌아옴
                

            }
        
        }

    }

    private void chekcMidPoint()
    {
        //_midPoint = (pos + pos2) / 2;
        // 귀찮더라도 x와 y를 나눠서 써야 될듯..
        _midPoint = new Vector2((pos.x + pos2.x) / 2, (pos.y + pos2.y) / 2+0.4f);//그래픽 좌표 보정값 0.4f를 해준다.
        Needle.transform.position = _midPoint;
        
    }
}