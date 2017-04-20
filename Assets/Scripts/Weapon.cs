using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{
    [SerializeField] int MaxMagazineSize, CurrentMagazineSize;
    [SerializeField] bool IsReloading;
    [SerializeField] float ReloadSpeed, FireRate, NextFireTime;

	// Use this for initialization
	void Start ()
    {
        MaxMagazineSize = 100;
        CurrentMagazineSize = MaxMagazineSize;

        ReloadSpeed = 1.0f;

        FireRate = 1.5f;

        NextFireTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        if (Input.GetMouseButtonDown(0))
            PerformWeaponOperations();

        DecrementNextFireTime();
	}

    public void PerformWeaponOperations()
    {
        if (NextFireTime <= 0)
        {
            CurrentMagazineSize--;
            RefreshNextFireTime();
        }
    }

    private void RefreshNextFireTime()
    {
        NextFireTime = 1;
    }

    private void DecrementNextFireTime()
    {
        if (NextFireTime > 0)
            NextFireTime -= Time.deltaTime * FireRate;
    }

    private IEnumerator FireWeapon()
    {
        //yield return new 
    }

    // Begins the reload coroutine. This is called after the user presses the reload button.
    public void Reload() { StartCoroutine(PerformReload()); }

    // Performs the reload coroutine - IsReloading becomes true, which interrupts any weapon operation, and waits for a variable amount of time (your reload speed) before
    // actually setting the Current Magazine Size to the Max Magazine Size.
    private IEnumerator PerformReload()
    {
        IsReloading = true;

        yield return new WaitForSeconds(ReloadSpeed);
        CurrentMagazineSize = MaxMagazineSize;

        IsReloading = false;
    }
}
