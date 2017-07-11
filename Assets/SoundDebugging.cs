using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDebugging : MonoBehaviour
{
    [SerializeField] int currentSound, maxSounds;
    AudioSource source;

	// Use this for initialization
	void Start ()
    {
        currentSound = 1;
        maxSounds = 2;

        source = GameObject.Find("Machine Gunner").GetComponent<AudioSource>();
        //Globals.PlaySoundClip(source, 3, 5);
        StartCoroutine(playSound());
    }
	
	// Update is called once per frame
	void Update ()
    {
        //StartCoroutine(playSound());
	}

    IEnumerator playSound()
    {
        do
        {
            Globals.PlaySoundClip(source, 5, currentSound);
            yield return new WaitForSeconds(4f);
            currentSound++;
        }
        while (currentSound != maxSounds + 1);
    }
}
