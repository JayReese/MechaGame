using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SoundManager : MonoBehaviour
{
    static List<SoundBank> Banks;

    private void Awake()
    {
        Banks = new List<SoundBank>();
        AllocateToSoundBanks();
    }

    // Use this for initialization
    void Start ()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AllocateToSoundBanks()
    {
        string[] dirs = Directory.GetDirectories("Assets/Resources/Sounds");

        foreach (string s in dirs)
            Banks.Add(new SoundBank(Path.GetFileName(s), Directory.GetFiles(s)));
    }

    public static AudioClip GetSoundClipForAllocation(byte indexOfList, byte indexOfSound)
    {
        return Banks[indexOfList].ReturnCorrectAudioClip(indexOfSound);
    }
}
