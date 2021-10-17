using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Performance
{
    public class CodeDictionary : MonoBehaviour
    {
        public static           bool                                           inPlay  = false;
        private static readonly List<string>                                   MarkLines  = new List<string>();
        private static readonly Regex                                          MarksRegex = new Regex( @"</?mark[^>]*>" );
        private static          Dropdown                                       _algDropdown;
        private static          List<string>                                   _keys;
        private static          Dictionary<string, string>                     _codeLines;
        private static          Dictionary<string, Dictionary<string, string>> _codeDict;

        private void Start()
        {
            _algDropdown = GameObject.Find( "Algorithm" ).GetComponent<Dropdown>();
            var json = Resources.Load<TextAsset>( "Json/CodeLines" );
            _codeDict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(
                json.text,
                new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto} );
        }

        private void Update()
        {
            if ( inPlay )
            {
                _codeLines = _codeDict[_algDropdown.options[_algDropdown.value].text];
                _keys = new List<string>( _codeLines.Keys );
                foreach ( var key in _keys )
                {
                    if ( MarkLines.Contains( key ) )
                    {
                        if ( !_codeLines[key].StartsWith( "<mark" ) )
                        {
                            switch ( key )
                            {
                                case "Swap":
                                case "Swap2":
                                case "Copy":
                                    _codeLines[key] = "<mark=#00ff1255>" + _codeLines[key] + "</mark>";
                                    break;
                                case "Selected":
                                    _codeLines[key] = "<mark>" + _codeLines[key] + "</mark>";
                                    break;
                                default:
                                    _codeLines[key] = "<mark>" + _codeLines[key] + "</mark>";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if ( _codeLines[key].StartsWith( "<mark" ) )
                            _codeLines[key] = MarksRegex.Replace( _codeLines[key], "" );
                    }
                }

                GetComponent<TextMeshProUGUI>().text = GetText( _codeLines );
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = "";
            }
        }

        public static void AddMarkLine( string key )
        {
            MarkLines.Add( key );
        }

        public static void RemoveMarkLine( string key )
        {
            MarkLines.Remove( key );
        }

        private static string GetText( Dictionary<string, string> lines )
        {
            var str = "";
            foreach ( var line in lines.Values )
            {
                str += line + "\r\n";
            }

            return str;
        }
    }
}