using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        //Quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }


    public void LoadByIndex(int iSceneIndex)
    {
        SceneManager.LoadScene(iSceneIndex);
    }

    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
