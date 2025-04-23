using UnityEngine;

public class Mover : MonoBehaviour
{

    [Header("Settings")]
    public float moveSpeed; // 건물 움직이는 속도 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.CalculateGameSpeed() * Time.deltaTime;
    }
}
