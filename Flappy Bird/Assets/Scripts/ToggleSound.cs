using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    [SerializeField] private Sprite[] musicBtnSprites, soundBtnSprites;
    [SerializeField] Image targetBtnMusic, targetBtnSound;

    void Update(){
        if(AudioManager.instance.musicOn()){
            targetBtnMusic.sprite = musicBtnSprites[0];
        }
        else{
            targetBtnMusic.sprite = musicBtnSprites[1];
        }

        if(AudioManager.instance.sfxOn()){
            targetBtnSound.sprite = soundBtnSprites[0];
        }
        else{
            targetBtnSound.sprite = soundBtnSprites[1];
        }
    }

    public void ChangeMusicBtnSprite()
    {
        AudioManager.instance.ToggleMusic();
        // if (targetBtnMusic.sprite == musicBtnSprites[0])
        // {
        //     targetBtnMusic.sprite = musicBtnSprites[1];
        //     return;
        // }
        // targetBtnMusic.sprite = musicBtnSprites[0];
    }

    public void ChangeSoundBtnSprite()
    {
        AudioManager.instance.ToggleSFX();
        // if (targetBtnSound.sprite == soundBtnSprites[0])
        // {
        //     targetBtnSound.sprite = soundBtnSprites[1];
        //     return;
        // }
        // targetBtnSound.sprite = soundBtnSprites[0];
    }
}
