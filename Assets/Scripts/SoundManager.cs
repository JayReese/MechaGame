using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SoundManager : MonoBehaviour
{
    public List<SoundBank> Bank;

    private void Awake()
    {
        Bank = new List<SoundBank>();
    }

    // Use this for initialization
    void Start ()
    {
        AllocateToSoundBanks();

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
            SoundBank sb = new SoundBank(Path.GetFileName(s));
            Bank.Add(sb);
        }

        Bank.ForEach(x => Debug.Log(x.BankName));
    }
}
