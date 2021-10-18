// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderMenu : MonoBehaviour
    {
        public GameObject sliderMenu;

        public void ShowHideMenu()
        {
            if ( sliderMenu != null )
            {
                var anim = sliderMenu.GetComponent<Animator>();
                if ( anim != null )
                {
                    if ( anim.GetBool( "Show" ) )
                    {
                        anim.SetBool( "Show", false );
                        // GameObject.Find( "SliderButton" ).GetComponentInChildren<Text>().text = "<<";
                    }
                    else
                    {
                        anim.SetBool( "Show", true );
                        // GameObject.Find( "SliderButton" ).GetComponentInChildren<Text>().text = ">>";
                    }
                }
            }
        }
    }
}