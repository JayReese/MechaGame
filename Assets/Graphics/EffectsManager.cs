using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsManager : MonoBehaviour
{
    #region Background Fields
    [SerializeField]
    Image backgroundImage;

    [SerializeField]
    bool backgroundIsFading;

    [SerializeField]
    int backgroundColorValue;

    int backgroundColorChangeSpeed = 2;
    #endregion

    #region PeoplePanel Fields
    [SerializeField]
    Image peoplePanelImage;

    [SerializeField]
    int peoplePanelColorValue;

    int peoplePanelColorChangeSpeed = 2;
    #endregion

    #region FadePanel Fields
    [SerializeField]
    Image fadePanelImage;

    [SerializeField]
    int fadePanelColorValue;

    int fadePanelColorChangeSpeed = 2;
    #endregion

    public enum States
    {
        FadeIn,
        MechaIn,
        PilotsIn,
        Idle
    }

    [SerializeField]
    States currentState;

    // Use this for initialization
    void Start()
    {
        currentState = States.FadeIn;

        #region Background Set Up
        backgroundIsFading = true;
        backgroundImage.color = new Color32(0, 255, 255, 255);
        backgroundColorValue = 0;
        #endregion

        #region Fade Panel Set Up
        fadePanelImage.color = new Color32(0, 0, 0, 255);
        fadePanelColorValue = 255;
        #endregion

        #region People Panel Set Up
        peoplePanelImage.color = new Color32(255, 255, 255, 0);
        peoplePanelColorValue = 0;
        #endregion
    }

    public static byte ToByte(int value)
    {
        return (byte)value;
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundEffects();
        FadePanelEffects();
        PeoplePanelEffects();
    }

    private void BackgroundEffects()
    {
        if (currentState == States.MechaIn)
        {
            backgroundColorValue = backgroundColorValue + backgroundColorChangeSpeed;
            backgroundImage.color = new Color32(ToByte(backgroundColorValue), 255, 255, 255);

            if (backgroundColorValue >= 255)
            {
                backgroundColorValue = 255;
                backgroundImage.color = new Color32(255, 255, 255, 255);
                currentState = States.PilotsIn;
            }
        }

        if (currentState == States.Idle)
        {
            #region VisorGlowingEffect
            if (backgroundColorValue > 255 && !backgroundIsFading)
            {
                backgroundIsFading = true;
                backgroundColorValue = 255;
                backgroundImage.color = new Color32(255, 255, 255, 255);
            }

            if (backgroundIsFading)
            {
                backgroundColorValue = backgroundColorValue - backgroundColorChangeSpeed;
                backgroundImage.color = new Color32(255, ToByte(backgroundColorValue), ToByte(backgroundColorValue), 255);
            }

            if (backgroundColorValue < 0 && backgroundIsFading)
            {
                backgroundIsFading = false;
                backgroundColorValue = 0;
                backgroundImage.color = new Color32(255, 0, 0, 255);
            }

            if (!backgroundIsFading)
            {
                backgroundColorValue = backgroundColorValue + backgroundColorChangeSpeed;
                backgroundImage.color = new Color32(255, ToByte(backgroundColorValue), ToByte(backgroundColorValue), 255);
            }
            #endregion
        }
    }

    private void PeoplePanelEffects()
    {
        if (currentState == States.PilotsIn)
        {
            peoplePanelColorValue = peoplePanelColorValue + peoplePanelColorChangeSpeed;
            peoplePanelImage.color = new Color32(255, 255, 255, ToByte(peoplePanelColorValue));

            if (peoplePanelColorValue >= 255)
            {
                peoplePanelImage.color = new Color32(255, 255, 255, 255);
                currentState = States.Idle;
            }
        }
    }

    private void FadePanelEffects()
    {
        if (currentState == States.FadeIn)
        {
            fadePanelColorValue = fadePanelColorValue - fadePanelColorChangeSpeed;
            fadePanelImage.color = new Color32(0, 0, 0, ToByte(fadePanelColorValue));

            if (fadePanelColorValue <= 0)
            {
                fadePanelImage.color = new Color32(0, 0, 0, 0);
                currentState = States.MechaIn;
            }
        }
    }
}
