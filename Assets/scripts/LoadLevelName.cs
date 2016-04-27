using UnityEngine;
using System.Collections;

public class LoadLevelName : MonoBehaviour {

    public void LoadByString(string level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
