using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    private AudioSource bgm;
    private string volumeKey = "Volume";
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        bgm = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        volumeSlider.value = PlayerPrefs.GetFloat(volumeKey);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SetVolume(volumeSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetVolume(float value)
    {
        bgm.volume = value;
        PlayerPrefs.SetFloat(volumeKey, value);
    }
}
