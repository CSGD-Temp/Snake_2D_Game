using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU_UI : MonoBehaviour
{
    [SerializeField] private GameObject _MainMenu_Panal;
    [SerializeField] private GameObject _LevelSelect_Panal;
    [SerializeField] private GameObject _Setings_Panal;
    [SerializeField] private GameObject _Credits_Panal;


    public void play()
    {
        _MainMenu_Panal.SetActive(false);
        _LevelSelect_Panal.SetActive(true);
       
    }
    public void quit()
    {
        Application.Quit();
    }
    public void back()
    {
        _MainMenu_Panal.SetActive(true);
        _Credits_Panal.SetActive(false);
        _LevelSelect_Panal.SetActive(false);
        _Setings_Panal.SetActive(false);
    }
    public void settings()
    {
        _Setings_Panal.SetActive(true);
        _MainMenu_Panal.SetActive(false);
    }
    public void credits()
    {
        _Credits_Panal.SetActive(true);
        _MainMenu_Panal.SetActive(false);
    }

}
