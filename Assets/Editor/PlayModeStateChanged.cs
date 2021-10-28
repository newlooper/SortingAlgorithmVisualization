using Performance;
using UnityEditor;
using UnityEngine;

// ensure class initializer is called whenever scripts recompile
namespace Editor
{
    [InitializeOnLoad]
    public static class PlayModeStateChanged
    {
        // register an event handler when the class is initialized
        static PlayModeStateChanged()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
        }

        private static void LogPlayModeState( PlayModeStateChange state )
        {
            if ( state == PlayModeStateChange.ExitingPlayMode )
            {
                CubeController.runLevel = 0;
                Time.timeScale = 0;
                Debug.Log( "Quit Play Mode." );
            }
        }
    }
}