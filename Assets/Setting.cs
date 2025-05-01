using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public static Setting Instance { get; private set; }
    Setting(){Instance = this;}
    public Button soundButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public bool isSoundOn 
    {
        get { return PlayerPrefs.GetInt("Sound",1) == 1; }
        set { PlayerPrefs.SetInt("Sound", value ? 1 : 0); }
    }
    void OnEnable()
    {
        soundButton.onClick.AddListener(SoundClick);
        if(isSoundOn)
        {
            AudioListener.volume = 1;
            soundButton.image.sprite = soundOnSprite;
        }
        else
        {
            AudioListener.volume = 0;
            soundButton.image.sprite = soundOffSprite;
        }
    }
    void OnDisable()
    {
        soundButton.onClick.RemoveListener(SoundClick);
    }
    public void SoundClick()
    {
        AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;
        soundButton.image.sprite = AudioListener.volume == 1 ? soundOnSprite : soundOffSprite;
        isSoundOn = AudioListener.volume == 1;
    }
}
