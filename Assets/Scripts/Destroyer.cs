using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 건물이 카메라 영역을 벗어나면 건물을 삭제
        if(transform.position.x < -12)
        {
            Destroy(gameObject);
        }
    }
}
