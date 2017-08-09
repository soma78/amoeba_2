using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pimpleScript : MonoBehaviour {

    public float m_currentHp;
    public float m_MaxHp;
    private bool canDecreaseHp;
    
    public Transform m_diamondTr;
    public GameObject m_particlePrefab;
    public GameObject m_particelPrefab2;

    void Start()
    {
        m_MaxHp = Random.Range(30f, 200f);
        m_currentHp = m_MaxHp;
        
        canDecreaseHp = false;
        m_diamondTr = transform.FindChild("Jewel").transform;

    }

	void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("TriggerEnter_1");
        if (col.tag == "Needle")
        {
            //Debug.Log("TriggerEnter_2");
            canDecreaseHp = true;
            StartCoroutine("DecreaseHp");
        }
    }

    IEnumerator DecreaseHp()
    {
        while (canDecreaseHp && GameManager.Instance.canDecreaseHp)
        {
            m_currentHp -= GameManager.Instance.m_Power;
            yield return new WaitForSeconds(0.4f);
            //Debug.Log("position : " + (m_MaxHp - m_currentHp) / 100 * 0.3f);
            m_diamondTr.localPosition= new Vector3(m_diamondTr.localPosition.x, (m_MaxHp - m_currentHp) /100 * 0.3f, -1f);
            //Tip : 자식 Transform이니까 그냥 Position으로 하면 안되고 localPosition으로 해야 된다.
            if(m_currentHp <= 0)
            {
                GameManager.Instance.CheckCurrentPimple();
                GameManager.Instance.ResetHand();
                Instantiate(m_particlePrefab, transform.position, Quaternion.identity);
                Instantiate(m_particelPrefab2);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        canDecreaseHp = false;
        //Debug.Log("TriggerExit");
    }

    //void Update()
    //{
    //    if (canDecreaseHp)
    //    {
    //        m_power += Time.deltaTime *0.3f;
    //        m_hp -= m_power;
    //    }
    //}
}
