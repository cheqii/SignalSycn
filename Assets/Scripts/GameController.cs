using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int currentSceneIndex;
    
    private PocketSignal pocket;
    
    [Header("Pocket Signal")]
    public bool isPocket;
    
    public bool isPocketDelay;
    public float pocketDelay;

    [Header("Receiver")]
    public bool isReceiver;

    [Header("Player Life")]
    public int maxLife;

    public int currentLife;

    public List<Image> heartImage;


    [Header("GameOver Stuff")]
    public GameObject gameOverPage;
    public bool isOver; 
    public Light2D attachedLight;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        currentLife = maxLife;
        
        isPocket = true;
        isReceiver = false;

        pocket = FindObjectOfType<PocketSignal>();
    }

    // Update is called once per frame
    void Update()
    {
        RestartAGameLevel();
        GameIsOver();
    }

    #region -Player Method-

    public void DecreaseLife(int value)
    {
        if (currentLife > 0)
        {
            currentLife -= value;
            heartImage[currentLife].color = new Color32(80, 80, 80, 255);
        }

        if (currentLife <= 0)
        {
            currentLife = 0;
            heartImage[currentLife].color = new Color32(80, 80, 80, 255);
        }
    }

    public IEnumerator PlayerControllerDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPocket = true;
        isReceiver = false;
        isPocketDelay = false;
    }
    // public void PlayerControllerDelay()
    // {
    //     Debug.Log("Player delay");
    //     isPocket = true;
    //     isReceiver = false;
    // }
    
    #endregion

    public void GameIsOver()
    {
        if (pocket == null && !isOver)
        {
            isOver = true;
            if (attachedLight != null)
            {
                Destroy(attachedLight);
            }
            gameOverPage.SetActive(true);
        }
    }

    void RestartAGameLevel()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pocket == null)
        {
            SceneManager.LoadSceneAsync(currentSceneIndex);
        }

        if (Input.GetKeyDown(KeyCode.R) && pocket != null)
        {
            SceneManager.LoadSceneAsync(currentSceneIndex);
        }
    }

    public void NextLevelScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex + 1);
    }
}
