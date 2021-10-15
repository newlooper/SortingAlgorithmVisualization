using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Performance
{
    public class CodeDictionary : MonoBehaviour
    {
        public static           bool         isPlaying = false;
        private static          Dropdown     _algDropdown;
        private static readonly List<string> MarkLines  = new List<string>();
        private static readonly Regex        MarksRegex = new Regex( @"</?mark[^>]*>" );

        private void Start()
        {
            _algDropdown = GameObject.Find( "Algorithm" ).GetComponent<Dropdown>();
        }

        private void Update()
        {
            if ( isPlaying )
            {
                var codeLines = CodeDict[_algDropdown.options[_algDropdown.value].text];

                foreach ( var key in codeLines.Keys.ToList() )
                {
                    if ( MarkLines.Contains( key ) )
                    {
                        if ( !codeLines[key].StartsWith( "<mark" ) )
                        {
                            switch ( key )
                            {
                                case "Swap":
                                case "Swap2":
                                case "Copy":
                                    codeLines[key] = "<mark=#00ff1255>" + codeLines[key] + "</mark>";
                                    break;
                                case "Selected":
                                    codeLines[key] = "<mark>" + codeLines[key] + "</mark>";
                                    break;
                                default:
                                    codeLines[key] = "<mark>" + codeLines[key] + "</mark>";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if ( codeLines[key].StartsWith( "<mark" ) )
                            codeLines[key] = MarksRegex.Replace( codeLines[key], "" );
                    }
                }

                GetComponent<TextMeshProUGUI>().text = GetText( codeLines );
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
            return lines.Values.Aggregate( "", ( current, line ) => current + ( line + "\r\n" ) );
        }

        private static readonly Dictionary<string, Dictionary<string, string>> CodeDict = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "Bubble", new Dictionary<string, string>
                {
                    {"Line1", "<color=blue>public static void</color> Sort( <color=blue>int</color>[] <color=#00ffffff>arr</color> )"},
                    {"Line2", "{"},
                    {"Line3", "	<color=blue>var</color> n = <color=#00ffffff>arr</color>.Length;"},
                    {"For", "	<color=blue>for</color> ( <color=blue>var</color> i = 0; i < n - 1; i++ )"},
                    {"Line5", "	{"},
                    {"For2", "		<color=blue>for</color> ( <color=blue>var</color> j = 0; j < n - i - 1; j++ )"},
                    {"Line7", "		{"},
                    {"Selected", "			<color=blue>if</color> ( <color=#00ffffff>arr</color>[j] > <color=#00ffffff>arr</color>[j + 1] )"},
                    {"Line9", "			{"},
                    {
                        "Swap",
                        "				( <color=#00ffffff>arr</color>[j], <color=#00ffffff>arr</color>[j + 1] ) = ( <color=#00ffffff>arr</color>[j + 1], <color=#00ffffff>arr</color>[j] );"
                    },
                    {"Line11", "			}"},
                    {"Line12", "		}"},
                    {"Line13", "	}"},
                    {"Line14", "}"},
                }
            },
            {
                "Cocktail", new Dictionary<string, string>
                {
                    {
                        "Line1",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> Sort( <color=blue>int</color>[] <color=#00ffffff>arr</color> )"
                    },
                    {"Line2", "{"},
                    {"Line3", "	<color=blue>var</color> swapped = <color=blue>true</color>;"},
                    {"Line4", "	<color=blue>var</color> start   = <color=red>0</color>;"},
                    {"Line5", "	<color=blue>var</color> end     = <color=#00ffffff>arr</color>.Length;"},
                    {"Line6", ""},
                    {"While", "	<color=blue>while</color> ( swapped )"},
                    {"Line8", "	{"},
                    {"Line9", "		swapped = <color=blue>false</color>;"},
                    {"For", "		<color=blue>for</color> ( <color=blue>var</color> i = start; i < end - <color=red>1</color>; ++i )"},
                    {"Line11", "		{"},
                    {"Selected", "			<color=blue>if</color> ( <color=#00ffffff>arr</color>[i] > <color=#00ffffff>arr</color>[i + <color=red>1</color>] )"},
                    {"Line13", "			{"},
                    {
                        "Swap",
                        "				( <color=#00ffffff>arr</color>[i], <color=#00ffffff>arr</color>[i + <color=red>1</color>] ) = ( <color=#00ffffff>arr</color>[i + <color=red>1</color>], <color=#00ffffff>arr</color>[i] );"
                    },
                    {"Line15", "				swapped = <color=blue>true</color>;"},
                    {"Line16", "			}"},
                    {"Line17", "		}"},
                    {"Line18", ""},
                    {"Line19", "		<color=blue>if</color> ( swapped == <color=blue>false</color> )"},
                    {"Line20", "			<color=blue>break</color>;"},
                    {"Line21", ""},
                    {"Line22", "		swapped = <color=blue>false</color>;"},
                    {"Line23", "		end -= <color=red>1</color>;"},
                    {"Line24", ""},
                    {"For2", "		<color=blue>for</color> ( <color=blue>var</color> i = end - <color=red>1</color>; i >= start; i-- )"},
                    {"Line26", "		{"},
                    {"Selected2", "			<color=blue>if</color> ( <color=#00ffffff>arr</color>[i] > <color=#00ffffff>arr</color>[i + <color=red>1</color>] )"},
                    {"Line28", "			{"},
                    {
                        "Swap2",
                        "				( <color=#00ffffff>arr</color>[i], <color=#00ffffff>arr</color>[i + <color=red>1</color>] ) = ( <color=#00ffffff>arr</color>[i + <color=red>1</color>], <color=#00ffffff>arr</color>[i] );"
                    },
                    {"Line30", "				swapped = <color=blue>true</color>;"},
                    {"Line31", "			}"},
                    {"Line32", "		}"},
                    {"Line33", "		start += <color=red>1</color>;"},
                    {"Line34", "	}"},
                    {"Line35", "}"},
                }
            },
            {
                "Comb", new Dictionary<string, string>()
                {
                    {"Line1", "<color=blue>static</color> <color=blue>int</color> GetNextGap( <color=blue>int</color> gap )"},
                    {"Line2", "{"},
                    {"Line3", "	gap = ( gap * <color=red>10</color> ) / <color=red>13</color>;"},
                    {"Line4", "	<color=blue>if</color> ( gap < <color=red>1</color> )"},
                    {"Line5", "	{"},
                    {"Line6", "		<color=blue>return</color> <color=red>1</color>;"},
                    {"Line7", "	}"},
                    {"Line8", ""},
                    {"Line9", "	<color=blue>return</color> gap;"},
                    {"Line10", "}"},
                    {"Line11", ""},
                    {
                        "Line12",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> Sort( <color=blue>int</color>[] <color=#00ffffff>arr</color> )"
                    },
                    {"Line13", "{"},
                    {"Line14", "	<color=blue>var</color> length = <color=#00ffffff>arr</color>.Length;"},
                    {"Line15", "	<color=blue>var</color> gap    = length;"},
                    {"Line16", ""},
                    {"Line17", "	<color=blue>var</color> swapped = <color=blue>true</color>;"},
                    {"Line18", ""},
                    {"While", "	<color=blue>while</color> ( gap != <color=red>1</color> || swapped )"},
                    {"Line20", "	{"},
                    {"Line21", "		gap = GetNextGap( gap );"},
                    {"Line22", "		swapped = <color=blue>false</color>;"},
                    {"Line23", ""},
                    {"For", "		<color=blue>for</color> ( <color=blue>var</color> i = <color=red>0</color>; i < length - gap; i++ )"},
                    {"Line25", "		{"},
                    {"Selected", "			<color=blue>if</color> ( <color=#00ffffff>arr</color>[i] > <color=#00ffffff>arr</color>[i + gap] )"},
                    {"Line27", "			{"},
                    {
                        "Swap",
                        "				( <color=#00ffffff>arr</color>[i], <color=#00ffffff>arr</color>[i + gap] ) = ( <color=#00ffffff>arr</color>[i + gap], <color=#00ffffff>arr</color>[i] );"
                    },
                    {"Line29", "				swapped = <color=blue>true</color>;"},
                    {"Line30", "			}"},
                    {"Line31", "		}"},
                    {"Line32", "	}"},
                    {"Line33", "}"},
                }
            },
            {
                "Gnome", new Dictionary<string, string>()
                {
                    {
                        "Line1",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> GnomeSort( <color=blue>int</color>[] <color=#00ffffff>arr</color>, <color=blue>int</color> length )"
                    },
                    {"Line2", "{"},
                    {"Line3", "	<color=blue>var</color> index = <color=red>0</color>;"},
                    {"While", "	<color=blue>while</color> ( index < length )"},
                    {"Line5", "	{"},
                    {"Line6", "		<color=blue>if</color> ( index == <color=red>0</color> )"},
                    {"Line7", "		{"},
                    {"Line8", "			index++;"},
                    {"Line9", "		}"},
                    {"Line10", ""},
                    {
                        "Selected",
                        "		<color=blue>if</color> ( <color=#00ffffff>arr</color>[index] >= <color=#00ffffff>arr</color>[index - <color=red>1</color>] )"
                    },
                    {"Line12", "		{"},
                    {"Line13", "			index++;"},
                    {"Line14", "		}"},
                    {"Line15", "		<color=blue>else</color>"},
                    {"Line16", "		{"},
                    {
                        "Swap",
                        "			( <color=#00ffffff>arr</color>[index], <color=#00ffffff>arr</color>[index - <color=red>1</color>] ) = ( <color=#00ffffff>arr</color>[index - <color=red>1</color>], <color=#00ffffff>arr</color>[index] );"
                    },
                    {"Line18", "			index--;"},
                    {"Line19", "		}"},
                    {"Line20", "	}"},
                    {"Line21", "}"},
                }
            },
            {
                "Heap", new Dictionary<string, string>()
                {
                    {
                        "Line1",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> Sort( <color=blue>int</color>[] <color=#00ffffff>arr</color> )"
                    },
                    {"Line2", "{"},
                    {"Line3", "	<color=blue>var</color> n = <color=#00ffffff>arr</color>.Length;"},
                    {"For", "	<color=blue>for</color> ( <color=blue>var</color> i = n / <color=red>2</color>; i >= <color=red>0</color>; i-- )"},
                    {"Line5", "	{"},
                    {"Line6", "		Heapify( <color=#00ffffff>arr</color>, n - <color=red>1</color>, i );"},
                    {"Line7", "	}"},
                    {"For2", "	<color=blue>for</color> ( <color=blue>var</color> i = n - <color=red>1</color>; i > <color=red>0</color>; i-- )"},
                    {"Line9", "	{"},
                    {
                        "Swap",
                        "		( <color=#00ffffff>arr</color>[i], <color=#00ffffff>arr</color>[<color=red>0</color>] ) = ( <color=#00ffffff>arr</color>[<color=red>0</color>], <color=#00ffffff>arr</color>[i] );"
                    },
                    {"Line11", "		Heapify( <color=#00ffffff>arr</color>, i - <color=red>1</color>, <color=red>0</color> );"},
                    {"Line12", "	}"},
                    {"Line13", "}"},
                    {"Line14", ""},
                    {
                        "Line15",
                        "<color=blue>static</color> <color=blue>void</color> Heapify( <color=blue>int</color>[] <color=#00ffffff>arr</color>, <color=blue>int</color> n, <color=blue>int</color> i )"
                    },
                    {"Line16", "{"},
                    {"Line17", "	<color=blue>var</color> max   = i;"},
                    {"Line18", "	<color=blue>var</color> left  = <color=red>2</color> * i + <color=red>1</color>;"},
                    {"Line19", "	<color=blue>var</color> right = <color=red>2</color> * i + <color=red>2</color>;"},
                    {"Line20", ""},
                    {"IF", "	<color=blue>if</color> ( left <= n && <color=#00ffffff>arr</color>[left] > <color=#00ffffff>arr</color>[max] ) {"},
                    {"Line22", "		max = left;"},
                    {"Line23", "	}"},
                    {"IF2", "	<color=blue>if</color> ( right <= n && <color=#00ffffff>arr</color>[right] > <color=#00ffffff>arr</color>[max] ) {"},
                    {"Line25", "		max = right;"},
                    {"Line26", "	}"},
                    {"Line27", "	<color=blue>if</color> ( max != i ) {"},
                    {
                        "Swap2",
                        "		( <color=#00ffffff>arr</color>[i], <color=#00ffffff>arr</color>[max] ) = ( <color=#00ffffff>arr</color>[max], <color=#00ffffff>arr</color>[i] );"
                    },
                    {"Line29", "		Heapify( <color=#00ffffff>arr</color>, n, max );"},
                    {"Line30", "	}"},
                    {"Line31", "}"},
                }
            },
            {
                "Insertion", new Dictionary<string, string>()
                {
                    {
                        "Line1",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> Sort( <color=blue>int</color>[] <color=#00ffffff>arr</color> )"
                    },
                    {"Line2", "{"},
                    {
                        "For",
                        "	<color=blue>for</color> ( <color=blue>var</color> i = <color=red>0</color>; i < <color=#00ffffff>arr</color>.Length - <color=red>1</color>; i++ )"
                    },
                    {"Line4", "	{"},
                    {"For2", "		<color=blue>for</color> ( <color=blue>var</color> j = i + <color=red>1</color>; j > <color=red>0</color>; j-- )"},
                    {"Line6", "		{"},
                    {"Compare", "			<color=blue>if</color> ( <color=#00ffffff>arr</color>[j - <color=red>1</color>] > <color=#00ffffff>arr</color>[j] )"},
                    {"Line8", "			{"},
                    {
                        "Swap",
                        "				( <color=#00ffffff>arr</color>[j - <color=red>1</color>], <color=#00ffffff>arr</color>[j] ) = ( <color=#00ffffff>arr</color>[j], <color=#00ffffff>arr</color>[j - <color=red>1</color>] );"
                    },
                    {"Line10", "			}"},
                    {"Line11", "		}"},
                    {"Line12", "	}"},
                    {"Line13", "}"},
                }
            },
            {
                "OddEven", new Dictionary<string, string>()
                {
                    {
                        "Line1",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> Sort( <color=blue>int</color>[] <color=#00ffffff>arr</color> )"
                    },
                    {"Line2", "{"},
                    {"Line3", "	<color=blue>var</color> sorted = <color=blue>false</color>;"},
                    {"While", "	<color=blue>while</color> ( !sorted )"},
                    {"Line5", "	{"},
                    {"Line6", "		sorted = <color=blue>true</color>;"},
                    {
                        "For",
                        "		<color=blue>for</color> ( <color=blue>var</color> i = <color=red>1</color>; i < <color=#00ffffff>arr</color>.Length - <color=red>1</color>; i += <color=red>2</color> )"
                    },
                    {"Line8", "		{"},
                    {"Selected", "			<color=blue>if</color> ( <color=#00ffffff>arr</color>[i] > <color=#00ffffff>arr</color>[i + <color=red>1</color>] )"},
                    {"Line10", "			{"},
                    {
                        "Swap",
                        "				( <color=#00ffffff>arr</color>[i], <color=#00ffffff>arr</color>[i + <color=red>1</color>] ) = ( <color=#00ffffff>arr</color>[i + <color=red>1</color>], <color=#00ffffff>arr</color>[i] );"
                    },
                    {"Line12", "				sorted = <color=blue>false</color>;"},
                    {"Line13", "			}"},
                    {"Line14", "		}"},
                    {"Line15", ""},
                    {
                        "For2",
                        "		<color=blue>for</color> ( <color=blue>var</color> i = <color=red>0</color>; i < <color=#00ffffff>arr</color>.Length - <color=red>1</color>; i += <color=red>2</color> )"
                    },
                    {"Line17", "		{"},
                    {"Selected2", "			<color=blue>if</color> ( <color=#00ffffff>arr</color>[i] > <color=#00ffffff>arr</color>[i + <color=red>1</color>] )"},
                    {"Line19", "			{"},
                    {
                        "Swap2",
                        "				( <color=#00ffffff>arr</color>[i], <color=#00ffffff>arr</color>[i + <color=red>1</color>] ) = ( <color=#00ffffff>arr</color>[i + <color=red>1</color>], <color=#00ffffff>arr</color>[i] );"
                    },
                    {"Line21", ""},
                    {"Line22", "				sorted = <color=blue>false</color>;"},
                    {"Line23", "			}"},
                    {"Line24", "		}"},
                    {"Line25", "	}"},
                    {"Line26", "}"},
                }
            },
            {
                "Quick", new Dictionary<string, string>()
                {
                    {
                        "Line1",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> QuickSort( <color=blue>int</color>[] <color=#00ffffff>arr</color>, <color=blue>int</color> left, <color=blue>int</color> right )"
                    },
                    {"Line2", "{"},
                    {"Line3", "	<color=blue>if</color> ( left >= right ) <color=blue>return</color>;"},
                    {"Line4", ""},
                    {"Line5", "	<color=blue>var</color> middleValue = <color=#00ffffff>arr</color>[( left + right ) / <color=red>2</color>];"},
                    {"Line6", "	<color=blue>var</color> cursorLeft  = left - <color=red>1</color>;"},
                    {"Line7", "	<color=blue>var</color> cursorRight = right + <color=red>1</color>;"},
                    {"While", "	<color=blue>while</color> ( <color=blue>true</color> )"},
                    {"Line9", "	{"},
                    {"Selected", "		<color=blue>while</color> ( <color=#00ffffff>arr</color>[++cursorLeft] < middleValue ) ;"},
                    {"Line11", ""},
                    {"Selected2", "		<color=blue>while</color> ( <color=#00ffffff>arr</color>[--cursorRight] > middleValue ) ;"},
                    {"Line13", ""},
                    {"IF", "		<color=blue>if</color> ( cursorLeft >= cursorRight )"},
                    {"Selected3", "			<color=blue>break</color>;"},
                    {"Line16", ""},
                    {
                        "Swap",
                        "		( <color=#00ffffff>arr</color>[cursorLeft], <color=#00ffffff>arr</color>[cursorRight] ) = ( <color=#00ffffff>arr</color>[cursorRight], <color=#00ffffff>arr</color>[cursorLeft] );"
                    },
                    {"Line18", "	}"},
                    {"Line19", ""},
                    {"Line20", "	QuickSort( <color=#00ffffff>arr</color>, left, cursorLeft - <color=red>1</color> );"},
                    {"Line21", "	QuickSort( <color=#00ffffff>arr</color>, cursorRight + <color=red>1</color>, right );"},
                    {"Line22", "}"},
                }
            },
            {
                "Selection", new Dictionary<string, string>()
                {
                    {
                        "Line1",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> Sort( <color=blue>int</color>[] <color=#00ffffff>arr</color> )"
                    },
                    {"Line2", "{"},
                    {"Line3", "	<color=blue>var</color> n = <color=#00ffffff>arr</color>.Length;"},
                    {"For", "	<color=blue>for</color> ( <color=blue>var</color> i = <color=red>0</color>; i < n - <color=red>1</color>; i++ )"},
                    {"Line5", "	{"},
                    {"Selected", "		<color=blue>var</color> min = i;"},
                    {"For2", "		<color=blue>for</color> ( <color=blue>var</color> j = i + <color=red>1</color>; j < n; j++ )"},
                    {"Line8", "		{"},
                    {"Selected2", "			<color=blue>if</color> ( <color=#00ffffff>arr</color>[j] < <color=#00ffffff>arr</color>[min] )"},
                    {"Line10", "			{"},
                    {"Selected3", "				min = j;"},
                    {"Line12", "			}"},
                    {"Line13", "		}"},
                    {
                        "Swap",
                        "		( <color=#00ffffff>arr</color>[min], <color=#00ffffff>arr</color>[i] ) = ( <color=#00ffffff>arr</color>[i], <color=#00ffffff>arr</color>[min] );"
                    },
                    {"Line15", "	}"},
                    {"Line16", "}"},
                }
            },
            {
                "MergeBottomUp", new Dictionary<string, string>()
                {
                    {
                        "Line1",
                        "<color=blue>public</color> <color=blue>static</color> <color=blue>void</color> Sort( <color=blue>int</color>[] <color=#00ffffff>arr</color> )"
                    },
                    {"Line2", "{"},
                    {"Line3", "	<color=blue>var</color> orderedArr = new <color=blue>int</color>[<color=#00ffffff>arr</color>.Length];"},
                    {
                        "For",
                        "	<color=blue>for</color> ( <color=blue>var</color> i = <color=red>2</color>; i < <color=#00ffffff>arr</color>.Length * <color=red>2</color>; i *= <color=red>2</color> )"
                    },
                    {"Line5", "	{"},
                    {
                        "For2",
                        "		<color=blue>for</color> ( <color=blue>var</color> j = <color=red>0</color>; j < ( <color=#00ffffff>arr</color>.Length + i - <color=red>1</color> ) / i; j++ )"
                    },
                    {"Line7", "		{"},
                    {"Line8", "			<color=blue>var</color> LEFT               = i * j;"},
                    {
                        "Line9",
                        "			<color=blue>var</color> MIDDLE             = LEFT + i / <color=red>2</color> >= <color=#00ffffff>arr</color>.Length ? ( <color=#00ffffff>arr</color>.Length - <color=red>1</color> ) : ( LEFT + i / <color=red>2</color> );"
                    },
                    {
                        "Line10",
                        "			<color=blue>var</color> RIGHT              = i * ( j + <color=red>1</color> ) - <color=red>1</color> >= <color=#00ffffff>arr</color>.Length ? ( <color=#00ffffff>arr</color>.Length - <color=red>1</color> ) : ( i * ( j + <color=red>1</color> ) - <color=red>1</color> );"
                    },
                    {"Line11", "			<color=blue>int</color> nextAuxiliaryIndex = LEFT, left = LEFT, mid = MIDDLE;"},
                    {"While", "			<color=blue>while</color> ( left < MIDDLE && mid <= RIGHT )"},
                    {"Line13", "			{"},
                    {"Line14", "				<color=blue>if</color> ( <color=#00ffffff>arr</color>[left] < <color=#00ffffff>arr</color>[mid] )"},
                    {"Line15", "				{"},
                    {"Pick", "					orderedArr[nextAuxiliaryIndex++] = <color=#00ffffff>arr</color>[left++];"},
                    {"Line17", "				}"},
                    {"Line18", "				<color=blue>else</color>"},
                    {"Line19", "				{"},
                    {"Pick2", "					orderedArr[nextAuxiliaryIndex++] = <color=#00ffffff>arr</color>[mid++];"},
                    {"Line21", "				}"},
                    {"Line22", "			}"},
                    {"Line23", ""},
                    {"Line24", "			<color=blue>while</color> ( left < MIDDLE )"},
                    {"Pick3", "				orderedArr[nextAuxiliaryIndex++] = <color=#00ffffff>arr</color>[left++];"},
                    {"Line26", "			<color=blue>while</color> ( mid <= RIGHT )"},
                    {"Pick4", "				orderedArr[nextAuxiliaryIndex++] = <color=#00ffffff>arr</color>[mid++];"},
                    {"Line28", ""},
                    {"Copy", "			Array.Copy( orderedArr, LEFT, <color=#00ffffff>arr</color>, LEFT, RIGHT - LEFT + <color=red>1</color> );"},
                    {"Line30", "		}"},
                    {"Line31", "	}"},
                    {"Line32", "}"},
                }
            },
        };
    }
}