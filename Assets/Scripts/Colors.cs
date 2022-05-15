using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Colors
{
    public static Dictionary<string, Color> colors = new Dictionary<string, Color>() {
        {"Blue", createColor(0,0,255,230)},
        {"Red", createColor(255,0,0,230)},
        {"Purple", createColor(255,0,255,230)},
        {"White", createColor(255,255,255,230)},
        {"Black", createColor(10,10,10,230)},
        {"Yellow", createColor(238,238,0,230)},
        {"Green", createColor (0,255,0,230) },
        {"Orange", createColor (241,54,10,230) },
    };

    private static Color createColor(int r, int g, int b, int a) { 
        Vector4 rgba = new Vector4(r, g, b,a) / 255f;
        return new Color(rgba[0], rgba[1], rgba[2], rgba[3]);
    }

    public static string AddColors(string beam, string stand) {
        if (beam == stand) {
            return beam;
        }
        switch (beam) 
        {
            case "Blue":
                switch (stand)
                {
                    case "Red":
                        return "Purple";
                    case "Purple":
                        return "Purple";
                    case "White":
                        return "White";
                    case "Yellow":
                        return "Green";
                    case "Green":
                        return "Green";
                    case "Orange":
                        return "White";
                };
                break;
            case "Red":
                switch (stand)
                {
                    case "Blue":
                        return "Purple";
                    case "Purple":
                        return "Purple";
                    case "White":
                        return "White";
                    case "Yellow":
                        return "Orange";
                    case "Green":
                        return "White";
                    case "Orange":
                        return "Orange";
                }
                break;

            case "Purple":
                switch (stand) {
                    case "Blue":
                        return "Purple";
                    case "Red":
                        return "Purple";
                    case "White":
                        return "White";
                    case "Yellow":
                        return "White";
                    case "Green":
                        return "White";
                    case "Orange":
                        return "White";
                }
                break;
            case "White":
                return "White";
            case "Yellow":
                switch (stand) {
                    case "Blue":
                        return "Green";
                    case "Red":
                        return "Orange";
                    case "Purple":
                        return "White";
                    case "White":
                        return "White";
                    case "Green":
                        return "Green";
                    case "Orange":
                        return "Orange";
                }
                break;
            case "Green":
                switch (stand) {
                    case "Blue":
                        return "Green";
                    case "Red":
                        return "White";
                    case "Purple":
                        return "White";
                    case "White":
                        return "White";
                    case "Yellow":
                        return "Green";
                    case "Orange":
                        return "White";
                }
                break;
            case "Orange":
                switch (stand) {
                    case "Blue":
                        return "White";
                    case "Red":
                        return "Orange";
                    case "Purple":
                        return "White";
                    case "White":
                        return "White";
                    case "Yellow":
                        return "Orange";
                    case "Green":
                        return "White";
                }
                break;
        }
        return "White";
    }

    public static string SubstractColors(string beam, string stand)
    {
        if (beam == stand)
        {
            return "Black";
        }
        switch (beam)
        {
            case "Blue":
                switch (stand)
                {
                    case "Red":
                        return "Blue";
                    case "Purple":
                        return "Black";
                    case "White":
                        return "Black";
                    case "Yellow":
                        return "Blue";
                    case "Green":
                        return "Black";
                    case "Orange":
                        return "Blue";
                };
                break;
            case "Red":
                switch (stand)
                {
                    case "Blue":
                        return "Red";
                    case "Purple":
                        return "Black";
                    case "White":
                        return "Black";
                    case "Yellow":
                        return "Red";
                    case "Green":
                        return "Red";
                    case "Orange":
                        return "Black";
                }
                break;

            case "Purple":
                switch (stand)
                {
                    case "Blue":
                        return "Red";
                    case "Red":
                        return "Blue";
                    case "White":
                        return "Black";
                    case "Yellow":
                        return "Purple";
                    case "Green":
                        return "Red";
                    case "Orange":
                        return "Blue";
                }
                break;
            case "White":
                switch (stand)
                {
                    case "Blue":
                        return "Orange";
                    case "Red":
                        return "Green";
                    case "Purple":
                        return "Yellow";
                    case "White":
                        return "Black";
                    case "Green":
                        return "Red";
                    case "Orange":
                        return "Blue";
                    case "Yellow":
                        return "Purple";
                }
                break;
            case "Yellow":
                switch (stand)
                {
                    case "Blue":
                        return "Yellow";
                    case "Red":
                        return "Yellow";
                    case "Purple":
                        return "Yellow";
                    case "White":
                        return "Black";
                    case "Green":
                        return "Black";
                    case "Orange":
                        return "Black";
                }
                break;
            case "Green":
                switch (stand)
                {
                    case "Blue":
                        return "Yellow";
                    case "Red":
                        return "Green";
                    case "Purple":
                        return "Yellow";
                    case "White":
                        return "Black";
                    case "Yellow":
                        return "Blue";
                    case "Orange":
                        return "Blue";
                }
                break;
            case "Orange":
                switch (stand)
                {
                    case "Blue":
                        return "Orange";
                    case "Red":
                        return "Yellow";
                    case "Purple":
                        return "Yellow";
                    case "White":
                        return "Black";
                    case "Yellow":
                        return "Red";
                    case "Green":
                        return "Red";
                }
                break;
        }
        return "White";
    }



    static public bool ContainsBlue(string color) {
        if (color == "Blue" || color == "White" || color == "Green" || color == "Purple") {
            return true;
        }
        return false;
    }

    static public bool ContainsRed(string color)
    {
        if (color == "Red" || color == "White" || color == "Orange" || color == "Purple")
        {
            return true;
        }
        return false;
    }

    static public bool ContainsYellow(string color)
    {
        if (color == "Yellow" || color == "White" || color == "Orange" || color == "Green")
        {
            return true;
        }
        return false;
    }
}
