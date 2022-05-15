using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingTexts
{
    public static string getText(int index) {
        switch (index){
            case 1: return "It is said, that the temple of Azshi is more than 20000 years old. Based on the text on the walls, Azshi was a goddess of light";
            case 2: return "According to the books, the light is composed of three basic colors. Red, Yellow and Blue";
            case 3: return "There are special structures in the temple, some of them can remove a color from the beam, other can add it.";
            case 4: return "Number of adventures tried to get through the tests in the temple, none of them succeeded.";
            case 5: return "People believe that there is a big treasure waiting for the one who passes all the tests.";
            default: return "Anyone can try their luck and try to venture to the depths of the temple of Azshi.";

        }




    }
}
