using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundBank
{
    public string BankName;
    List<AudioClip> AudioCollection;

    public SoundBank(string name, string[] filesToAllocate)
    {
        BankName = name;

        AudioCollection = new List<AudioClip>();
        AllocateSoundFilesToBank(filesToAllocate);
    }

    void AllocateSoundFilesToBank(string[] files)
    {
        foreach(string s in files)
        {
            if(!s.Contains(".meta"))
            {
                string path = s.Replace("\\", "/").Replace("Assets/Resources/", string.Empty).Replace(".wav", string.Empty);
                AudioCollection.Add(Resources.Load(path) as AudioClip);
            }
        }
    }

    public AudioClip ReturnCorrectAudioClip(byte index)
    {
        return AudioCollection[index];
    }
}
