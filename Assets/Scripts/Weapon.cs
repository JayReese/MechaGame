using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{
    [SerializeField] int MaxMagazineSize, CurrentMagazineSize, BurstCount;
    [SerializeField] bool IsReloading, TriggerPulled;
    public bool IsFiring { get; private set; }
    [SerializeField] float ReloadSpeed, FireRate, ShotInterval, NextFireTime;
    [SerializeField] GameObject WeaponProjectile;
    [SerializeField] Transform WeaponEmitter;
    public Transform LockOnTarget;

	// Use this for initialization
	protected void Start ()
    {
        SetDefaults();
	}
	
	// Update is called once per frame
	protected void Update ()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R) && !IsReloading)
            Reload();

        TriggerPulled = Input.GetMouseButton(0);
#endif

        if (TriggerPulled && NextFireTime <= 0 && CurrentMagazineSize > 0)
            PerformWeaponOperations();

        if (!IsFiring) DecrementNextFireTime();
	}

    /// <summary>
    /// Performs the necessary weapon operations.
    /// </summary>
    public void PerformWeaponOperations()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        // Ensures that the next fire counter isn't decremting before the shots are fired. 
        // This way, you can't just shoot without the warmup in between. 
        // We can take this out if it proves too slow for gameplay, but this is generally how a rifle should work.
        IsFiring = true;
        RefreshFiringMechanism();

        // A flexible way of implementing burst shots. If the weapon doesn't burst, it'll only call itself once since BurstCount
        // would always equal one.
        for (int i = 0; i < BurstCount; i++)
        {
            yield return new WaitForSeconds(ShotInterval);
            FireProjectile();
            ReduceMagazine();
        }

        // Finally, the Next Fire is polled once again, and the weapon will take its time to allow you to fire once more.
        IsFiring = false;
    }

    private void FireProjectile()
    {
        GameObject g = WeaponProjectile;
        //g.GetComponent<Projectile>().Origin = transform.parent;
        g.GetComponent<Projectile>().LockOnTarget = transform.parent.GetComponentInChildren<CameraMovement>().CurrentLockOnTarget;

        Instantiate(g, WeaponEmitter.position, WeaponEmitter.transform.rotation);
    }

    private void ReduceMagazine()
    {
        CurrentMagazineSize--;
    }
    
    // The function that refreshes the dependent firing variables.
    private void RefreshFiringMechanism()
    {
        NextFireTime = 1;
    }

    // The function that actually decrements the firing variable if it's above 0.
    private void DecrementNextFireTime()
    {
        if (NextFireTime > 0)
            NextFireTime -= Time.deltaTime * FireRate;
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

    // Sets the defaults of the weapon.
    void SetDefaults()
    {
        MaxMagazineSize = 10;
        CurrentMagazineSize = MaxMagazineSize;

        ReloadSpeed = 1.0f;

        FireRate = 1.5f;

        // Future-proofing this. 
        // What this is saying is, for some reason if anyone ever sets BurstCount to zero for a mecha (meaning they interpret 
        // it to mean "don't have burst fire"), then it'll set BurstCount to 1 since the loop would need to run exactly
        // one time. 
        // In addition, a burst count of one will automatically set the shot interval to zero. That can be taken out if a 
        // mechanic demands for it.
        BurstCount = BurstCount == 0 ? 1 : BurstCount;
        ShotInterval = BurstCount == 1 ? 0 : ShotInterval;

        NextFireTime = 0;
        IsFiring = false;

        WeaponProjectile = Resources.Load("Prefabs/Test Projectile") as GameObject;
        WeaponEmitter = transform.FindChild("Weapon Emitter");
    }
}
