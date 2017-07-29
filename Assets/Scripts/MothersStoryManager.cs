using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MothersStoryManager : InstancedManager
{
    List<string> _textLines;

    new void Awake()
    {
        //base.Awake();
        _textLines = new List<string>();
    }

	// Use this for initialization
	void Start ()
    {
        OpenStreamReader();
        StartCoroutine(DisplayStoryText());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OpenStreamReader()
    {
        string text;

        using (StreamReader reader = new StreamReader(Globals.ReturnChosenDirectory("Resources\\MothersStory.txt")))
        {
            while ((text = reader.ReadLine()) != null)
                _textLines.Add(text);
        }

        //_textLines.ForEach(x => Debug.Log(x));

        RemoveWhiteSpaceFromList();
    }

    private void RemoveWhiteSpaceFromList()
    {
        for(int i = 0; i < _textLines.Count; i++)
        {
            if (_textLines[i] == string.Empty)
                _textLines.RemoveAt(i); 
        }
    }

    IEnumerator DisplayStoryText()
    {
        for(int i = 0; i < _textLines.Count; i++)
        {
            string textToDisplay = _textLines[i].Remove(0);
            //Debug.Log(textToDisplay);
            //Debug.Log(textToDisplay);
            yield return new WaitForSeconds(4f);
        }
        
    }
}
