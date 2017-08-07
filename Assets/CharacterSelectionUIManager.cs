using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterSelectionUIManager : MonoBehaviour
{
    [SerializeField] ImageHolder CharacterImageHolder;

	// Use this for initialization
	void Start ()
    {
        CharacterImageHolder = new ImageHolder(GetMechaNames());
	}
	
	// Update is called once per frames
	void Update ()
    {
		
	}

    List<string> GetMechaNames()
    {
        string lineToRead = string.Empty;
        List<string> mechaNames = new List<string>();

        using (StreamReader sr = new StreamReader(Globals.ReturnChosenDirectory(@"\Scripts\Mechas.txt")))
        {
            while(true)
            {
                lineToRead = sr.ReadLine();

                if (lineToRead == null)
                {
                    break;
                }

                mechaNames.Add(lineToRead);
            }
            
        }

        return mechaNames;
    }
}
