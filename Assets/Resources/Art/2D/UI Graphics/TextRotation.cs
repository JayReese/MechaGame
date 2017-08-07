using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRotation : MonoBehaviour
{
    [SerializeField]
    GameObject Title;

    [SerializeField]
    GameObject TitleText;

    public RectTransform titleTrans;

    public RectTransform textTrans;

    float rotationSpeed = 20;

    public bool settingUp;

	// Use this for initialization
	void Start ()
    {
        titleTrans = Title.GetComponent<RectTransform>();
        textTrans = TitleText.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (titleTrans.rotation.y <= 0 && textTrans.rotation.y <= 0)
        {
            titleTrans.Rotate(0, 1 * rotationSpeed*Time.deltaTime, 0, 0);
            textTrans.Rotate(0, 1 * rotationSpeed * Time.deltaTime, 0, 0);
        }
	}
}
