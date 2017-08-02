using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHolder : MonoBehaviour
{
    [SerializeField]
    List<Sprite> Sprites;

    [SerializeField]
    GameObject RedStartFlag;

    [SerializeField]
    GameObject WhiteStartFlag;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivateRedStart()
    {
        RedStartFlag.SetActive(true);
    }

    public void ActivateWhiteStart()
    {
        WhiteStartFlag.SetActive(true);
    }
}
