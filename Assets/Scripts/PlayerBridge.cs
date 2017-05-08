using UnityEngine;
using System.Collections;

public class PlayerBridge : MonoBehaviour
{
    public float interval = .8f;
    float time = 0;
    bool pressedOnce = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Cardboard.SDK.Triggered)
        {
            //If the player presses the button twice fast enough within the interval of time
            if (pressedOnce && Time.time<(time + interval)) {
                BridgeMaker.bridgeOn();
            }

            //Set the time to when the player triggered the button
            time = Time.time;

            //set PressedOnce to the opposite
            pressedOnce = !pressedOnce;

        }

    }
}