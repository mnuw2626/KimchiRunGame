using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

// ���� ���¸� ����ȭ��(Intro), �÷�����(Playing), ����(Dead)
public enum GameState
{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State = GameState.Intro; //�ʱ� ���� ����

    public float PlayStartTime; // ���ʵ��� �÷��̾ ����ִ���(����)

    public int Lives = 3; // �÷��̾� ��� 3��

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner; // ��ֹ� ������Ʈ ����
    public GameObject FoodSpawner; // ���� ������Ʈ ����
    public GameObject GoldenSpawner; // Ȳ�ݹ��� ������Ʈ ����
    public Player PlayerScript; // �÷��̾� ����

    public TMP_Text scoreText; // ����

    private void Awake()
    {
        if (Instance == null) { 
            Instance = this;
        }
    }

    void Start()
    {
        IntroUI.SetActive(true); // ����ȭ�� Ȱ��ȭ
    }

    float CalculateScore()
    {
        return Time.time - PlayStartTime; // ����� �ð��� ��ȯ
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore"); // ��ǻ�� �ϵ忡 ������ ����(����)
        
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

    // ���� �ӵ�
    public float CalculateGameSpeed()
    {
        if(State != GameState.Playing) // �÷������� �ƴ� ��
        {
            return 5f;
        }
        float speed = 8f + (1f * Mathf.Floor(CalculateScore() / 10f)); // 10�ʸ��� 1�� �ӵ��� �ö�
        return Mathf.Min(speed, 30f); // �ִ� 30f �ӵ�
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
            State = GameState.Playing; // ���� ���¸� �÷���������
            IntroUI.SetActive(false); // ����ȭ�� ��Ȱ��ȭ
            EnemySpawner.SetActive(true); // �� ��ü Ȱ��ȭ
            FoodSpawner.SetActive(true); // ���ľ����� Ȱ��ȭ
            GoldenSpawner.SetActive(true); // Ȳ�ݹ���(����)������ Ȱ��ȭ
            PlayStartTime = Time.time; // ���� ���� �ð�
        }
        if (State == GameState.Playing && Lives == 0) // �÷����� ���¿��� ����� 0�̸�
        {
            PlayerScript.KillPlayer(); // �÷��̾� ����
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            DeadUI.SetActive(true);
            State = GameState.Dead; // ���� ���¸� �����
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) // �����
        {
            SceneManager.LoadScene("main");
        }
    }
}
