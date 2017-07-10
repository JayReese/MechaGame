using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        // Sorts the audio collection by the name of the file, so as to best organize it.
        AudioCollection = AudioCollection.OrderBy(x => x.name).ToList();
    }

    public AudioClip ReturnCorrectAudioClip(int index)
    {
        return AudioCollection[index];
    }
}
