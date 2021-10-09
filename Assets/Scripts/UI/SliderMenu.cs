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
                        GameObject.Find( "SliderButton" ).GetComponentInChildren<Text>().text = "<<";
                    }
                    else
                    {
                        anim.SetBool( "Show", true );
                        GameObject.Find( "SliderButton" ).GetComponentInChildren<Text>().text = ">>";
                    }
                }
            }
        }
    }
}