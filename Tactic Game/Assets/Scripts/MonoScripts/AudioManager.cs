using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Private
    [Header("--------- Audio Source ---------")]
    [SerializeField] private AudioSource musicSource;  // For Music like BG 
    [SerializeField] private AudioSource SFXSource;  // For SFX Clips

    // Public
    [Header("--------- Audio Clip ---------")]
    public AudioClip background;  // Background Music
    public AudioClip mouseClick;  // MouseClick SFX
    

    private void Start()
    {
        // Playing BGM on the Start
        musicSource.clip = background;
        musicSource.Play();
    }

    // Playing SFX in oneShot
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip); 
    }
}
