using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int PlayerID;
    public float MaxFuel, CurrentFuel;
    public PlayerState States;

	// Use this for initialization
	void Start ()
    {
        MaxFuel = 3;
        CurrentFuel = MaxFuel;
        States = PlayerState.ON_GROUND;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckForStateBasedFunctions();
	}

    void CheckForStateBasedFunctions()
    {
        if (States == PlayerState.BOOSTING) ChangeFuel(1);
        else ChangeFuel(-1);
    }

    void ChangeFuel(float multiplier)
    {
        if (multiplier == 1 && CurrentFuel > 0 || multiplier == -1 && CurrentFuel < MaxFuel)
            CurrentFuel -= Time.deltaTime * 1.5f * multiplier;

        CurrentFuel = States == PlayerState.ON_GROUND && CurrentFuel > MaxFuel ? MaxFuel : CurrentFuel;
    }
}