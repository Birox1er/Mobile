using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Achievement
{
    // Start is called before the first frame update
    internal static void HandleAchievemen(string v)
    {
        if (SceneManager.GetActiveScene().buildIndex > 5 && GglManager.successn)
        {
            Social.ReportProgress(v, 100.0f, (bool success) => {
                if (success == true)
                    LevelManager.Acchievement();
            });
        }
    }
}
