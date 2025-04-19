using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay; //새로운 객체를 만들기 위해서 최소 몇 초 기다리는 지
    public float maxSpawnDelay;//새로운 객체를 만들기 위해서 최대 몇 초 기다리는 지

    [Header("References")]
    public GameObject[] gameObjects; // 객체들

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void OnDisable()
    {
        CancelInvoke(); // 생성 비활성화
    }

    void Spawn()
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
