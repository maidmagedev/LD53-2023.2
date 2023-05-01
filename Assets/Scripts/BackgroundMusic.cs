using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] List<AudioClip> Tracks = new();

    AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void AudioToggle()
    {
        AudioListener.pause = !AudioListener.pause;
    }
    private void Awake()
    {
        int numMusic = FindObjectsOfType<BackgroundMusic>().Length;
        if (numMusic > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        myAudio.clip = Tracks[SceneManager.GetActiveScene().buildIndex];
        myAudio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        // you can check the index and change the audio to whatever you want, this is just an example
        /*if ((SceneManager.GetActiveScene().buildIndex == level1) && mayPlay)
        {
            mayPlay = false;
            // should play boss music
            myAudio.clip = MainTheme;
            myAudio.Play();
        }*/
    }
}
