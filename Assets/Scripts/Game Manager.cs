using UnityEngine;
using UnityEngine.SceneManagement;

// 게임 상태를 시작화면(Intro), 플레이중(Playing), 종료(Dead)
public enum GameState
{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State = GameState.Intro; //초기 게임 상태

    public int Lives = 3; // 플레이어 목숨 3개

    [Header("References")]
    public GameObject IntroUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public Player PlayerScript;

    private void Awake()
    {
        if (Instance == null) { 
            Instance = this;
        }
    }

    void Start()
    {
        IntroUI.SetActive(true); // 시작화면 활성화
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) {
            State = GameState.Playing; // 게임 상태를 플레이중으로
            IntroUI.SetActive(false); // 시작화면 비활성화
            EnemySpawner.SetActive(true); // 적 객체 활성화
            FoodSpawner.SetActive(true); // 음식아이템 활성화
            GoldenSpawner.SetActive(true); // 황금배추(무적)아이템 활성화
        }
        if (State == GameState.Playing && Lives == 0) // 플레이중 상태에서 목숨이 0이면
        {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            State = GameState.Dead; // 게임 상태를 종료로
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) // 재시작
        {
            SceneManager.LoadScene("main");
        }
    }
}
