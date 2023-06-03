using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoladLevels_UI : MonoBehaviour
{
    public void loadLevel_1()
    {
        SceneManager.LoadScene(1);
    }
    public void loadLevel_2()
    {
        SceneManager.LoadScene(2);
    }
    public void loadLevel_3()
    {
        SceneManager.LoadScene(3);
    }
    public void loadLevel_4()
    {
        SceneManager.LoadScene(4);
    }
    public void loadLevel_5()
    {
        SceneManager.LoadScene(5);
    }
    public void loadLevel_6()
    {
        SceneManager.LoadScene(6);
    }
    public void loadLevel_7()
    {
        SceneManager.LoadScene(7);
    }
    public void loadLevel_8()
    {
        SceneManager.LoadScene(8);
    }
    //-------------------------------------------

    public Button[] LevelButtons;

    private void Start()
    {
        int LevelReched = PlayerPrefs.GetInt("LevelReched", 1); 

        for (int i = 0; i < LevelButtons.Length; i++)
        {
            if(i + 1 > LevelReched)
            LevelButtons[i].interactable = false;
        }
    }
}
