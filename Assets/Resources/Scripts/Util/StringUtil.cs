using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class StringUtil
{
    public static string getDays(int value)
    {
        switch (value)
        {
            case 0: return "��";

            case 1: return "ȭ";

            case 2: return "��";

            case 3: return "��";

            case 4: return "��";

            case 5: return "��";

            case 6: return "��";

            default: return "errorData";
        }
    }
    
}
