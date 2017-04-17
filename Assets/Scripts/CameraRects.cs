using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The rects and various positional values for the camera. This class is called when the camera is first created, and 
/// the values within 
/// </summary>

public class CameraRects
{ 
    public List<float[]> TwoPlayerCamRects { get; private set; }
    public List<float[]> FourPlayerCamRects { get; private set; }

    public CameraRects()
    {
        AddTwoPlayerCamRects();
        AddFourPlayerCamRects();
    }

    void AddTwoPlayerCamRects()
    {
        TwoPlayerCamRects = new List<float[]>();

        TwoPlayerCamRects.Add(new float[] { 0, 0.5f });     // Camera 1
        TwoPlayerCamRects.Add(new float[] { 0, 0 });        // Camera 2
    }

    void AddFourPlayerCamRects()
    {
        FourPlayerCamRects = new List<float[]>();

        FourPlayerCamRects.Add(new float[] { 0, 0.5f, 0.5f });    // Camera 1
        FourPlayerCamRects.Add(new float[] { 0.5f, 0.5f, 0.5f }); // Camera 2
        FourPlayerCamRects.Add(new float[] { 0, 0, 0.5f });       // Camera 3
        FourPlayerCamRects.Add(new float[] { 0.5f, 0, 0.5f });    // Camera 4
    }
}
