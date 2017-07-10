using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDebugging : MonoBehaviour
{
    int currentSound, maxSounds;
    AudioSource source;

	// Use this for initialization
	void Start ()
    {
        currentSound = 0;
        maxSounds = 7;

        source = GameObject.Find("Machine Gunner").GetComponent<AudioSource>();
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
            yield return new WaitForSeconds(4f);
            Globals.PlaySoundClip(source, 1, currentSound);

            currentSound++;
        }
        while (currentSound != maxSounds);
    }
}
