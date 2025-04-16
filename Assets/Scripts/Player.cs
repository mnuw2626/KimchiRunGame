using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce; // ���� ��ġ

    [Header("References")]
    public Rigidbody2D PlayerRigidBody; //�÷��̾� ��
    public Animator PlayerAnimator; // �÷��̾� �ִϸ��̼�

    private bool isGrounded = true; // �ٴڿ� �ִ��� ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // �÷��̾ �ٴڿ� �ְ� �����̽��ٸ� ������
        {
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse); // ���� ���� ���� - JumpForce ��ŭ ����(Y��)�� Impulse(�������� ��)Ÿ���� Force(��)�� ��
            isGrounded = false; // �ٴڿ� ����
            PlayerAnimator.SetInteger("state", 1); // ���� �ִϸ��̼� ���� ����
        }

    }

    void OnCollisionEnter2D(Collision2D collision) //��� �浹�Ͽ����� �˼��ִ� �Լ�
    {
        if (collision.gameObject.name == "Platform") // �ٴڿ� �浹��
        {
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2); // ���� �ִϸ��̼� ���� ����
            }
            isGrounded = true; // �ٴڿ� �浹�ϸ� �ٴڿ� ����
           
        }
    }
}
