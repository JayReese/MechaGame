using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        {
            Banks.Add(new SoundBank(Path.GetFileName(s), Directory.GetFiles(s)));
        }

        // Puts the banks in alphanumerical order. This is extremely important and can make or break the system.
        Banks.Sort((x, y) => string.Compare(x.BankName, y.BankName));

        //Banks.ForEach(x => Debug.Log(x.BankName));
    }

    public static AudioClip GetSoundClipForAllocation(int indexOfList, int indexOfSound)
    {
        return Banks[indexOfList].ReturnCorrectAudioClip(indexOfSound);
    }
}
