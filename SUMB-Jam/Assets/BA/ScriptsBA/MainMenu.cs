using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene(2);
        UnityEngine.Cursor.visible = false;

    }
    public void How2Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void ret2Menu()
    {
        SceneManager.LoadScene(0);
    }

}
