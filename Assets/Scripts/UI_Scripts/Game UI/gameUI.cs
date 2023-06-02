using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gameUI : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOver;
    private bool _isPaused = false;
    public PlayerScript playerScript;

    private void Start() {
        playerScript = FindObjectOfType<PlayerScript>();
    }
    private void Update() {
        if(!playerScript.isPlayerDead){

            if(Input.GetKeyDown(KeyCode.Escape)){

               if(_isPaused){
                Resume();
               }
               else{
                Pause();
               }
            }
        }
        else{
             StartCoroutine(GameOver(0.8f));
        }
    }//Update
    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }//Resume
    private void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }
    public void Exit(){
        SceneManager.LoadScene(0);
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        playerScript.isPlayerDead = false;
    }

    private IEnumerator GameOver(float delay){

        yield return new WaitForSeconds(delay);
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }
}//Class
