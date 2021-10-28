// Copyright (c) 2021 Dylan Cheng (https://github.com/newlooper). All rights reserved.
// Use of this source code is governed by a MIT-style
// license that can be found in the LICENSE file.

using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderMenu : MonoBehaviour
    {
        public GameObject sliderMenu;

        public GameObject speed;
        public GameObject count;
        public GameObject max;
        public GameObject min;

        public void ShowHideMenu()
        {
            if ( sliderMenu != null )
            {
                var anim = sliderMenu.GetComponent<Animator>();
                if ( anim != null )
                {
                    anim.SetBool( "Show", !anim.GetBool( "Show" ) );
                }
            }
        }

        private void Update()
        {
            speed.GetComponentInChildren<Text>().text = LeanLocalization.GetTranslationText( "UI.Menu.Speed" ) + speed.GetComponent<Slider>().value;
            count.GetComponentInChildren<Text>().text = LeanLocalization.GetTranslationText( "UI.Menu.Count" ) + count.GetComponent<Slider>().value;
            max.GetComponentInChildren<Text>().text = LeanLocalization.GetTranslationText( "UI.Menu.Max" ) + max.GetComponent<Slider>().value;
            min.GetComponentInChildren<Text>().text = LeanLocalization.GetTranslationText( "UI.Menu.Min" ) + min.GetComponent<Slider>().value;
        }
    }
}