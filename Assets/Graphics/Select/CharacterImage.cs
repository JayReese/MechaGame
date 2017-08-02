using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterImage : MonoBehaviour
{
    public enum State
    {
        GoingUp,
        GoingDown
    }

    public enum Team
    {
        Red,
        White
    }

    public enum Status
    {
        Choosing,
        Locked
    }

    Status currentStatus;

    public State currentState;

    public Team team;

    public State startingState; 



    int movementValue;

    int movementSpeed = 15;

    Image image;


    public bool testButton = false;

	// Use this for initialization
	void Start ()
    {
        currentState = startingState;
        movementValue = 100;
        image = GetComponent<Image>();
	}
	
    public void LockinChoice()
    {
        currentStatus = Status.Locked;

        if (team == Team.Red)
        {
            image.color = new Color32(255, 0, 0, 255);
        }

        if (team == Team.White)
        {
            image.color = new Color32(255, 255, 255, 255);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        FloatingMotion();
        if(testButton)
        {
            LockinChoice();
        }
    }

    private void FloatingMotion()
    {
        if (currentState == State.GoingUp)
        {
            movementValue--;
            transform.Translate(0, movementSpeed * Time.deltaTime, 0);

            if (movementValue <= 0)
            {
                movementValue = 100;
                currentState = State.GoingDown;
            }
        }

        if (currentState == State.GoingDown)
        {
            movementValue--;
            transform.Translate(0, -1 * movementSpeed * Time.deltaTime, 0);

            if (movementValue <= 0)
            {
                movementValue = 100;
                currentState = State.GoingUp;
            }
        }
    }
}
