using Ommy.Audio;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int CurrentLevel
    {
        get
        {
            return PlayerPrefs.GetInt("CurrentLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
            PlayerPrefs.Save();
        }
    }
    public GameObject[] levels;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
        // Load the current level from PlayerPrefs
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        // Deactivate all levels
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }
        // Activate the current level
        if (CurrentLevel < levels.Length)
        {
            levels[CurrentLevel].SetActive(true);
        }
        else
        {
            Debug.LogError("Current level exceeds the number of available levels.");
        }
    }
    public void LevelComplete()
    {
        // Increment the level
        CurrentLevel++;
        if(CurrentLevel >= levels.Length)
        {
            CurrentLevel = 0; // Reset to the first level if all levels are completed
        }
        AudioManager.Instance.PlaySFX(SFX.win);
        UIManager.instance.LevelComplete();
        Time.timeScale = 0;
    }
    public void LevelFail()
    {
        AudioManager.Instance.PlaySFX(SFX.fail);
        UIManager.instance.LevelFail();
        Time.timeScale = 0;
    }
}
