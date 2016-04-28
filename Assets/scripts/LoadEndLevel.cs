using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class LoadEndLevel : MonoBehaviour
{
    [SerializeField] private int deathRec;
    [SerializeField] private float timeRec;

    public Collider2D player;

    private Collider2D coll;

    void Start()
    {
        coll = this.GetComponent<Collider2D>();
    }

    void Update()
    {
        if (coll.IsTouching(player))
        {
            if (PlayerPrefs.GetInt("cleanRun") == 1 && PlayerPrefs.GetInt("deaths") < deathRec && PlayerPrefs.GetFloat("time") < timeRec)
            {
                SceneManager.LoadScene("SarcasticWin");
            } else
            {
                SceneManager.LoadScene("BasicWin");
            }
        }
    }
}
