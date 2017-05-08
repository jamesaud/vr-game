// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CubeFunction : MonoBehaviour, ICardboardGazeResponder
{
    int colors = 1;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    void LateUpdate()
    {
        Cardboard.SDK.UpdateState();
        if (Cardboard.SDK.BackButtonPressed)
        {
            Application.Quit();
        }
    }

    //Switch the color to alternate with the bridge type
   // bool switchColor=false;
    public void SetGazedAt(bool gazedAt)
    {
        BridgeMaker.NextBridgeType();
        nextColor();
    }

    void nextColor() {
        if (colors == 2)
        {
            colors = 0;
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (colors == 1)
        {
            colors++;
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        else if (colors == 0) {
            colors++;
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

   

    #region ICardboardGazeResponder implementation

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see CardboardGaze).
    public void OnGazeEnter()
    {
        SetGazedAt(true);
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit()
    {
    }

    // Called when the Cardboard trigger is used, between OnGazeEnter
    /// and OnGazeExit.
    public void OnGazeTrigger()
    {
    }

    #endregion
}
