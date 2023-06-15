using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Achievement
{
    // Start is called before the first frame update
    internal static void HandleAchievemen(string v)
    {
        Social.ReportProgress(v, 100.0f, (bool success) => {
            // handle success or failure
        });
    }
}
