using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTrigger : MonoBehaviour
{
    public AudioMixerSnapshot MixerSnapshot;
    public float fadeIntoMusicTime = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            MixerSnapshot.TransitionTo(fadeIntoMusicTime);
            Debug.Log("MusicTriggerTriggered");
        }
    }
    
    
}
