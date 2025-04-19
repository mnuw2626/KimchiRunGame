using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int Lives = 3; // �÷��̾� ��� 3��

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
        IntroUI.SetActive(true); // ����ȭ�� Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space)) {
            State = GameState.Playing; // ���� ���¸� �÷���������
            IntroUI.SetActive(false); // ����ȭ�� ��Ȱ��ȭ
            EnemySpawner.SetActive(true); // �� ��ü Ȱ��ȭ
            FoodSpawner.SetActive(true); // ���ľ����� Ȱ��ȭ
            GoldenSpawner.SetActive(true); // Ȳ�ݹ���(����)������ Ȱ��ȭ
        }
        if (State == GameState.Playing && Lives == 0) // �÷����� ���¿��� ����� 0�̸�
        {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            State = GameState.Dead; // ���� ���¸� �����
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space)) // �����
        {
            SceneManager.LoadScene("main");
        }
    }
}
