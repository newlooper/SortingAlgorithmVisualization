// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using UnityEngine;

public class SkyboxCamera : MonoBehaviour
{
    private const float X = 15f;
    private       float Y;
    private const float Z = 0f;


    // Update is called once per frame
    private void Update()
    {
        if ( Y <= 360f )
        {
            Y += .5f * Time.unscaledDeltaTime;
            transform.rotation = Quaternion.Euler( new Vector3( X, Y, Z ) );
        }
        else
        {
            Y = 0f;
        }
    }
}