using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Pocket Signal")]
    public bool isPocket;

    [Header("Receiver")]
    public bool isReceiver;

    [Header("Player Life")]
    public int maxLife;
    public int currentLife;

    public List<Image> heartImage;

    private PocketSignal pocket;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        
        isPocket = true;
        isReceiver = false;

        pocket = FindObjectOfType<PocketSignal>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pocket == null) return;
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

    #endregion

    public void GameIsOver()
    {
        if(currentLife <= 0) Destroy(pocket.gameObject);
    }

    public void NextLevelScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex + 1);
    }
}
