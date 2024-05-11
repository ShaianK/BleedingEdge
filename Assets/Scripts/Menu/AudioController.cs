using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Audio sources for music and button sounds effect
    public AudioSource menuMusic;
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        // Play the menu music
        menuMusic.playOnAwake = true;
    }

    // Set the volume of the music
    public void setMusicVolume (float musicVolume) {
        menuMusic.volume = musicVolume;
    }

    // Set the volume of the sound effects
    public void setSoundEffectVolume (float sfxVolume) {
        sfx.volume = sfxVolume;
    }
}
