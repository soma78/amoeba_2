using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	void Awake()
    {
        Screen.SetResolution(720, 1280, true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
