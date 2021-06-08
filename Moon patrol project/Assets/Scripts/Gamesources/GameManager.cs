using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [Header("Transition settings")]
    public Animator transition;
    [SerializeField] float transitionTime;

    [Header("Input options")]
    public KeyCode rightMove;
    public KeyCode leftMove;
    public KeyCode jump;
    public KeyCode fire;
    public KeyCode pause;

    public int playerLifes;

    bool getScene;

    private void Awake()
    {
        getScene = true;

        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != null)
        {
            Destroy(gameObject);
        }

        rightMove = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", rightMove.ToString()));
        leftMove = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", leftMove.ToString()));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", jump.ToString()));
        fire = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("fireKey", fire.ToString()));
        pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pauseKey", pause.ToString()));
    }
    void Start()
    {
        playerLifes = 0;
    }

    private void Update()
    {
        if (getScene)
        {
            if (SceneManager.GetActiveScene().name == "game")
            {
                Cursor.visible = false;
                getScene = false;
            }
            else
            {
                Cursor.visible = true;
                getScene = false;
            }
        }
    }

    public void LoadScene(int levelIndex)
    {
        StartCoroutine(LoadLevelCourutine(levelIndex));
    }
    IEnumerator LoadLevelCourutine(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
        getScene = true;
    }
}
