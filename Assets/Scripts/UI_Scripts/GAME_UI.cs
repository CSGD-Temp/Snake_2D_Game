﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GAME_UI : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }
    public void ResumeGame(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    void PauseGame(){
        pauseMenuUI.SetActive(true);
        Time.timeScale =0f;
        isPaused = true;
    }
    public void RestartGame(){
        Time.timeScale = 1f;
        Scene currentScens = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScens.buildIndex);
        print("Game Restart");
    }
    public void ExitToMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}//Class