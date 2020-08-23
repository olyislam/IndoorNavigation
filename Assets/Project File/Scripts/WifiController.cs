using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WifiController : MonoBehaviour
{

    public WifiSignal wifiSignal;
    const float METERS_TO_FEET = 3.28f;
    int frameCount = 0;

    string Mac;
    float currSignal = 0, distance;
    int frequency, link_speed;
    public Text Mac_text, Frequency_text, Signal_text, Link_speed_text, Signal_Count_text, distance_text;


    // Update is called once per frame
    void Update()
    {
        //print current signal
        currSignal = Mathf.Lerp(currSignal, wifiSignal.GetCurrSignal(), Time.deltaTime * 10f);
        frameCount++;
        if (frameCount % 10 == 0)
        {
            frameCount = 0;
            distance = CalcDistance() * METERS_TO_FEET;
        }
        set_value();
        show_value();
    }

    void set_value()
    {
        Mac = wifiSignal.GetMacAddress();
        frequency = wifiSignal.GetFrequency();
        link_speed = wifiSignal.GetLinkSpeed();
    }

    void show_value()
    {
        Mac_text.text = "Mac: " + Mac;
        Frequency_text.text = "F(MHz):" + frequency;
        Signal_text.text = "Range: " + currSignal;
        Link_speed_text.text = "L SP: " + link_speed;
        distance_text.text = "distance: " + distance;
    }



    ///returns distance in meters
    private float CalcDistance()
    {
        float signalLevelInDb = Mathf.RoundToInt(currSignal);
        int freqInMHz = wifiSignal.GetFrequency();
        float exp = (27.55f - (20f * Mathf.Log10(freqInMHz)) + Math.Abs(signalLevelInDb)) / 20.0f;
        return Mathf.Pow(10.0f, exp);
    }
}


