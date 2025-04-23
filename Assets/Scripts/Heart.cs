using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart; // 차 있는 하트
    public Sprite OffHeart; // 비여있는 하트

    public int LiveNumber;

    public SpriteRenderer SpriteRenderer; //Sprite(하트들)를 화면에 그려주는 역할

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Lives >= LiveNumber)
        {
            SpriteRenderer.sprite = OnHeart;
        }
        else
        {
            SpriteRenderer.sprite = OffHeart;
        }
    }
}
