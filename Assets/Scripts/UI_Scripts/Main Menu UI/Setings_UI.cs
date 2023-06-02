using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setings_UI : MonoBehaviour
{
    [SerializeField] private AudioMixer _aMixer;
    [SerializeField] private Slider _Music;
    [SerializeField] private Slider _Sounds;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("GAME_Sounds"))
        {
            _aMixer.SetFloat("GAME_Sounds", 0f);
            _aMixer.SetFloat("PLAYER_Sounds", 0f);
        }
        else LOAD();
    }

    public void GameMusic()
    {
        _aMixer.SetFloat("GAME_Sounds", _Music.value);
    }

    public void PlayerSounds()
    {
        _aMixer.SetFloat("PLAYER_Sounds", _Sounds.value);
    }

    void LOAD()
    {
        _Music.value = PlayerPrefs.GetFloat("GAME_Sounds");
        _Sounds.value = PlayerPrefs.GetFloat("PLAYER_Sounds");
    }
    public void SAVE()
    {
        PlayerPrefs.SetFloat("GAME_Sounds", _Music.value);
        PlayerPrefs.SetFloat("PLAYER_Sounds", _Sounds.value);
    }
}
