using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class themeSystem  
{
    static Color32 IdleDark = new Color32(40, 40, 40, 179);
    static Color32 IdleLight = new Color32(200, 200, 200, 255);
    static Color32 backgroundDark = new Color32(24, 24, 24, 255);
    static Color32 backgroundLight = new Color32(222, 222, 222, 255);
    static Color32 circleZeroLight = new Color32(200, 200, 200, 255);
    static Color32 circleZeroDark = new Color32(0, 0, 0, 255);
    static Color32 circlePositiveDark = new Color32(80, 80, 80,255);
    static Color32 circllePositiveLight = new Color32(222, 222, 222, 255);


    public static Color32 getIdleTheme(string theme)
    {
        if (theme == "dark")
            return IdleDark;
        else if (theme == "light")
            return IdleLight;
        else throw new Exception("Wrong color parametre");

    }

    public static Color32 getBackgroundTheme(string theme)
    {
        if (theme == "dark")
            return backgroundDark;
        else if (theme == "light")
            return backgroundLight;
        else throw new Exception("Wrong color parametre");

    }

    public static Color32 getCircleZero(string theme)
    {
        if (theme == "dark")
            return circleZeroDark;
        else if (theme == "light")
            return circleZeroLight;
        else throw new Exception("Wrong color parametre");
    }

    public static Color32 getCirclePositive(string theme)
    {
        if (theme == "dark")
            return circlePositiveDark;
        else if (theme == "light")
            return circllePositiveLight;
        else throw new Exception("Wrong color parametre");
    }
    
}
