using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameState gameState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        gameState = GameState.Mainmenu;
    }
    public GameObject gameOverPanel, gameWinPanel,Tut, helpPanel;
    public TMP_Text levelNoTxt;
    private void Start() 
    {
        levelNoTxt.text = $"Level {GameManager.instance.CurrentLevel+1}";  
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameState == GameState.Mainmenu) 
        {
            gameState = GameState.GamePlay;
            Tut.SetActive(false);
        }
    }
    public void ShowHelp(bool show)
    {
        helpPanel.SetActive(show);
        Time.timeScale = show ? 0 : 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameState = GameState.GamePlay;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameState = GameState.GamePlay;
    }
    public void LevelFail()
    {
        gameState = GameState.LevelFail;
        gameOverPanel.SetActive(true);
        PlayerMovement.instance.IsWalking = false;
        PlayerMovement.instance.PlayerAnim.SetBool("isRunning", false);
    }
    public void LevelComplete()
    {
        gameState = GameState.LevelComplete;
        gameWinPanel.SetActive(true);
        PlayerMovement.instance.IsWalking = false;
        PlayerMovement.instance.PlayerAnim.SetBool("isRunning", false);
    }
}
public enum GameState
{ 
   Mainmenu,
   GamePlay,
   LevelComplete,
   LevelFail

}