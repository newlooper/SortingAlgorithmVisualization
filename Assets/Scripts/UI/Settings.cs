// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Settings : MonoBehaviour
    {
        void Start()
        {
            var v = GetComponent<Slider>().value;

            switch ( gameObject.name )
            {
                case "SliderMin":
                    setMin( v );
                    break;
                case "SliderMax":
                    setMax( v );
                    break;
                case "SliderCount":
                    setCount( v );
                    break;
                case "SliderSpeed":
                    setSpeed( v );
                    break;
            }
        }

        public void setMin( float v )
        {
            GetComponentInChildren<Text>().text = "Min: " + v;
        }

        public void setMax( float v )
        {
            GetComponentInChildren<Text>().text = "Max: " + v;
        }

        public void setCount( float v )
        {
            GetComponentInChildren<Text>().text = "Count: " + v;
        }

        public void setSpeed( float v )
        {
            GetComponentInChildren<Text>().text = "Speed: " + v;
        }
    }
}