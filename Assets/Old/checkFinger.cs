using UnityEngine;
using System.Collections;
using System;

public class checkFinger : MonoBehaviour {

    public TouchTest instance;
    bool _shortDistance;

	void Start () {
        instance = GameObject.Find("GameManger").GetComponent<TouchTest>();
        instance._score = 0;
        _shortDistance = false;
	}

    
	void OnTriggerEnter2D(Collider2D col)
    {
        //if(col.tag == "Needle")//gameobject도 붙여야 된다.
        if(col.gameObject.tag =="Needle")
        {
            Debug.Log("Score UP~~~~~~~~~~~");
            CheckScore();
        }


    }

	void Update () {

        //CheckDistance();

	
	}

    void CheckDistance()
    {
        if(instance._distance < 2.5f)
        {
            _shortDistance = true;
            CheckScore();
        }
    }

    private void CheckScore()
    {
        Debug.Log("Distance =" + instance._distance);
       //if(instance._distance < 3f && instance._distance >2f)//TouchTest에서 distance의 선언이 public이 아니여서 안보였다.Static은 안써줘도 되는건가??
       //{
            if (_shortDistance == true)
            {

                instance._score += 100;
                _shortDistance = false; //점수 한번 획득 하고 나면 false해줘서 중복 점수 획득 안됨

                //우선은 거리체크만 하고 이거 통과 하면, 좌표 체크도 구현 해야 됨

                Destroy(gameObject);//여드름 오브젝트 사라짐
                                    //Destroy(this)로 하면 안사라짐
        //    }

        }
    }
}
