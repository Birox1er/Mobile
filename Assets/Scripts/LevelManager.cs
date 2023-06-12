using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private int currentLevel = 1;
    private int maxLevel = 10; 

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LevelCompleted()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            UnlockNextLevel();
        }
        else
        {
            Debug.Log("Tous les niveaux ont été terminés !");
        }
    }

    private void UnlockNextLevel()
    {
        
    }
}
