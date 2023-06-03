using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gameUI : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameWin;
    public GameObject gameOver;
    private bool _isPaused = false;
    public PlayerScript playerScript;
    [SerializeField] private Text score_text;
    [SerializeField] private Slider slider;

    public int Score_per_food;
    int Score;
    private void Start() {
        playerScript = FindObjectOfType<PlayerScript>();
    }
    private void Update() {
        if(playerScript.isPlayerWin){
            StartCoroutine(GameWin(0.8f));
        }
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
        _Score();
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
    public void NextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        Time.timeScale = 1f;
        _isPaused = false;
        
    }

    private IEnumerator GameWin(float delay){
        yield return new WaitForSeconds(delay);
        gameWin.SetActive(true);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LevelReched", currentSceneIndex + 1);
        Time.timeScale = 0f;
        _isPaused = true;
    }
    private IEnumerator GameOver(float delay){

        yield return new WaitForSeconds(delay);
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }
    private void _Score()
    {
        Score = playerScript.eatFoodCount * Score_per_food;
        score_text.text = "Score " + Score;

        slider.maxValue = playerScript._maxFoodCanEat;
        slider.value = playerScript.eatFoodCount;
    }
}//Class
