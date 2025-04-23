using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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

    public float PlayStartTime; // 몇초동안 플레이어가 살아있는지(점수)

    public int Lives = 3; // 플레이어 목숨 3개

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner; // 장애물 오브젝트 관련
    public GameObject FoodSpawner; // 음식 오브젝트 관련
    public GameObject GoldenSpawner; // 황금배추 오브젝트 관련
    public Player PlayerScript; // 플레이어 관련

    public TMP_Text scoreText; // 점수

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

    float CalculateScore()
    {
        return Time.time - PlayStartTime; // 경과한 시간을 반환
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore"); // 컴퓨터 하드에 점수를 저장(동기)
        
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    // 게임 속도
    public float CalculateGameSpeed()
    {
        if(State != GameState.Playing) // 플레이중이 아닐 시
        {
            return 5f;
        }
        float speed = 8f + (1f * Mathf.Floor(CalculateScore() / 10f)); // 10초마다 1씩 속도가 올라감
        return Mathf.Min(speed, 30f); // 최대 30f 속도
    }

    // Update is called once per frame
    void Update()
    {
       
        if (State == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        }
        else if (State == GameState.Dead)
        {
            scoreText.text = "High Score: " + GetHighScore();
        }

        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing; // 게임 상태를 플레이중으로
            IntroUI.SetActive(false); // 시작화면 비활성화
            EnemySpawner.SetActive(true); // 적 객체 활성화
            FoodSpawner.SetActive(true); // 음식아이템 활성화
            GoldenSpawner.SetActive(true); // 황금배추(무적)아이템 활성화
            PlayStartTime = Time.time; // 게임 시작 시간
        }
        if (State == GameState.Playing && Lives == 0) // 플레이중 상태에서 목숨이 0이면
        {
            PlayerScript.KillPlayer(); // 플레이어 죽음
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            DeadUI.SetActive(true);
            State = GameState.Dead; // 게임 상태를 종료로
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) // 재시작
        {
            SceneManager.LoadScene("main");
        }
    }
}
