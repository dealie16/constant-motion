using UnityEngine;
using System.Collections;

public class LoadLevelName : MonoBehaviour {

    public void LoadByString(string level)
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
