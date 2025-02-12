using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneUIManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetVolumeSlider(volumeSlider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
