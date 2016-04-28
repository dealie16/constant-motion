using UnityEngine;
using System.Collections;

public class LoadEndLevel : MonoBehaviour
{
    [SerializeField] private int deathRec;
    [SerializeField] private float timeRec;

    public void LoadEnd()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("cleanRun") == 1 && PlayerPrefs.GetInt("deaths") < deathRec && PlayerPrefs.GetFloat("time") < timeRec)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SarcasticWin");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BasicWin");
        }
    }
}
