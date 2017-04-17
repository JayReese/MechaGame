using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        TwoPlayerCamRects.Add(new float[] { 0, 0.5f });     // Cam 1
        TwoPlayerCamRects.Add(new float[] { 0, 0 });        // Cam 2
    }

    void AddFourPlayerCamRects()
    {
        FourPlayerCamRects = new List<float[]>();

        FourPlayerCamRects.Add(new float[] { 0, 0.5f, 0.5f });    // Cam 1
        FourPlayerCamRects.Add(new float[] { 0.5f, 0.5f, 0.5f }); // Cam 2
        FourPlayerCamRects.Add(new float[] { 0, 0, 0.5f });       // Cam 3
        FourPlayerCamRects.Add(new float[] { 0.5f, 0, 0.5f });    // Cam 4
    }
}
