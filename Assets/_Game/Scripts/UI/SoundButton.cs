using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Sprite soundOnSprite; 
    public Sprite soundOffSprite; 
    public AudioSource audioSource; 
    private bool isSoundOn = true; 

    private void Start()
    {
        UpdateSoundButton();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn; 


        audioSource.enabled = isSoundOn;

        UpdateSoundButton();
    }

    private void UpdateSoundButton()
    {

        Image buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }
    }
}
