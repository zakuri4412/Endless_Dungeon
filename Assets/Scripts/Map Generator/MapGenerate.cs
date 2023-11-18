using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] sceneNames;
    GameObject player;
    public int testFloor;

    public GameObject LoadingScreen;
    public Image Loadingbar;

    [SerializeField] Transform spawnPoint;
    GameManager gameManager;
    const string defaultSaveFile = "save";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            wrapper.Load();
            Time.timeScale = 1;
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            gameManager.isNewGame = false;
            if (SceneManager.GetActiveScene().name == "Home")
            {
                CharacterController playerCurrentFloor = player.GetComponent<CharacterController>();
                int floor = 0;
                playerCurrentFloor.SetCurrentFloor(floor);
                StartCoroutine(Transitoin("Dungeon Scene"));
            }
            else
            {
                LoadRandomScene();
            }
            
        }
    }

    public void LoadGameManager()
    {
        gameManager.LoadGame();
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile, LoadingScreen, Loadingbar);
        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        wrapper.Save();
    }

    public void NewGameManager()
    {
        gameManager.NewGame();
        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        wrapper.Delete();
        StartCoroutine(Transitoin("Home"));
    }

    void LoadRandomScene()
    {
        if (player == null) return;
        CharacterController playerCurrentFloor = player.GetComponent<CharacterController>();
        if (sceneNames.Length > 0)
        {
            int randomIndex = Random.Range(0, sceneNames.Length);
            string randomSceneName = sceneNames[randomIndex];
            int floor = playerCurrentFloor.GetCurrentFloor() + 1;
            playerCurrentFloor.SetCurrentFloor(floor);
            StartCoroutine(Transitoin(randomSceneName));
        }
    }

    public void BackHome()
    {
        gameManager.LoadGame();
        StartCoroutine(Transitoin("Home"));
    }

    public IEnumerator Transitoin(string randomSceneName)
    {
        Time.timeScale = 0;
        DontDestroyOnLoad(gameObject);
        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        if (!gameManager.isNewGame)
        {
            wrapper.Save();
        }
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(randomSceneName);
        LoadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Loadingbar.fillAmount = progressValue;
            yield return null;
            UpdatePlayer();
        }
        yield return null;
        Destroy(gameObject);


    }

    private void UpdatePlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;
        if (spawnPoint == null) return;
        player.GetComponent<NavMeshAgent>().enabled = false;
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
        player.GetComponent<NavMeshAgent>().enabled = true;
        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        wrapper.Save();
    }
}
