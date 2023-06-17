using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    private const string UnlockKeyPrefix = "LevelUnlock_";
    private int totalLevels = 10; // Nombre total de niveaux dans le jeu
    public int levelNumber = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        UnlockLevel(0);
    }

    public void UnlockLevel(int levelIndex)
    {
        string unlockKey = UnlockKeyPrefix + levelIndex.ToString();
        PlayerPrefs.SetInt(unlockKey, 1); // Débloque le niveau en enregistrant la valeur 1 pour la clé correspondante
        PlayerPrefs.Save(); // Sauvegarde les modifications
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        string unlockKey = UnlockKeyPrefix + levelIndex.ToString();
        return PlayerPrefs.HasKey(unlockKey); // Vérifie si la clé du niveau est présente dans PlayerPrefs
    }

    public void ClearLevelProgress()
    {
        // Réinitialise le déblocage de tous les niveaux
        for (int i = 1; i < totalLevels; i++)
        {
            string unlockKey = UnlockKeyPrefix + i.ToString();
            PlayerPrefs.DeleteKey(unlockKey);
        }
        PlayerPrefs.Save(); // Sauvegarde les modifications
    }

    public void GetLevelProgress()
    {
        // Affiche dans la console le déblocage de tous les niveaux
        for (int i = 0; i < totalLevels; i++)
        {
            string unlockKey = UnlockKeyPrefix + i.ToString();
            Debug.Log("Level " + i + " unlocked: " + IsLevelUnlocked(i));
        }
    }
}
