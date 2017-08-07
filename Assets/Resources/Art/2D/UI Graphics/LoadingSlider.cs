using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSlider : MonoBehaviour
{
    [SerializeField]
    Slider whiteSlider;

    [SerializeField]
    Slider redSlidier;

    public float whiteRotationSpeed = 10;

    public float redRotationSpeed = -10;

    public float sliderFillRate = .001f;

	// Use this for initialization
	void Start ()
    {
        whiteSlider.value = 0;
        redSlidier.value = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        whiteSlider.value = whiteSlider.value + sliderFillRate;
        redSlidier.value = redSlidier.value + sliderFillRate;

        whiteSlider.transform.Rotate(0, 0, whiteRotationSpeed);
        redSlidier.transform.Rotate(0, 0, redRotationSpeed);
    }
}
