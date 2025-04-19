using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay; //���ο� ��ü�� ����� ���ؼ� �ּ� �� �� ��ٸ��� ��
    public float maxSpawnDelay;//���ο� ��ü�� ����� ���ؼ� �ִ� �� �� ��ٸ��� ��

    [Header("References")]
    public GameObject[] gameObjects; // ��ü��

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void OnDisable()
    {
        CancelInvoke(); // ���� ��Ȱ��ȭ
    }

    void Spawn()
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
