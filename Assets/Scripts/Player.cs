using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce; // ���� ��ġ

    [Header("References")]
    public Rigidbody2D PlayerRigidBody; //�÷��̾� ��
    public Animator PlayerAnimator; // �÷��̾� �ִϸ��̼�

    public BoxCollider2D PlayerCollider;

    private bool isGrounded = true; // �ٴڿ� �ִ��� ����

    private bool isInvincible = false; // ���� ����

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

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit()
    {
        GameManager.Instance.Lives -= 1;
    }

    void Heal()
    {
        GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1); //�Լ� �̿� -> ���� �ΰ� �� ���� ���� ���� ���
    }

    void StartInvincible() 
    {
        isInvincible = true;
        Invoke("StopInvincible", 10f); // 10�� �Ŀ� StopInvincible() ȣ��
    }

    void StopInvincible()
    {
        isInvincible = false;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "enemy")
        {
            if (!isInvincible) { // ������ �ƴ� ��
                Destroy(collider.gameObject); // (Player�� �΋H�� ��� ������Ʈ��)������� ��
                Hit();
            }
        }
        else if (collider.gameObject.tag == "food")
        {
            Destroy(collider.gameObject);
            Heal();
        }
        else if (collider.gameObject.tag == "golden")
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}
