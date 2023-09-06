using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject Panel;

    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public bool isPanelActive;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);    
    }

    public void OnButtonClick()
    {
        if (isPanelActive)
        {
            Panel.SetActive(false);
        }
        else
        {
            Panel.SetActive(true);
        }

        isPanelActive = !isPanelActive;
    }

    public void PlaySound()
    {
        AudioSource.PlayOneShot(AudioClip, 0.35f);
    }
}
