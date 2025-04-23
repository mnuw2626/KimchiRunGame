using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart; // �� �ִ� ��Ʈ
    public Sprite OffHeart; // ���ִ� ��Ʈ

    public int LiveNumber;

    public SpriteRenderer SpriteRenderer; //Sprite(��Ʈ��)�� ȭ�鿡 �׷��ִ� ����

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
