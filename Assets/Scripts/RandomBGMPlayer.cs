using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBGMPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<AudioClip> BGMs = new List<AudioClip>();
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            RandomPlay();
        }
    }

    void RandomPlay()
    {
        audio.clip = BGMs[Random.Range(0, BGMs.Count)];
        audio.Play();
    }
}
