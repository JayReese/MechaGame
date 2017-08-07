using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHolder
{
    [SerializeField]
    List<Sprite> MechaSelectionImages;

    [SerializeField]
    GameObject RedStartFlag;

    [SerializeField]
    GameObject WhiteStartFlag;

    public ImageHolder(List<string> mechaNames)
    {
        MechaSelectionImages = new List<Sprite>();
        GetAllMechaSelectionImages(mechaNames);
    }

    public void ActivateRedStart()
    {
        RedStartFlag.SetActive(true);
    }

    public void ActivateWhiteStart()
    {
        WhiteStartFlag.SetActive(true);
    }

    void GetAllMechaSelectionImages(List<string> mechaNames)
    {
        foreach (string s in mechaNames)
        {
            if (s == string.Empty)
                break;

            //Debug.Log(s);
            MechaSelectionImages.Add(Resources.Load(string.Format(@"Art\2D\UI Graphics\Character Select\Mecha{0}SelectImage", s), typeof(Sprite)) as Sprite);
        }
    }
}
