using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundBank
{
    public string BankName;
    public List<AudioClip> AudioCollection;

    public SoundBank(string name)
    {
        BankName = name;
        AudioCollection = new List<AudioClip>();
    }
}
