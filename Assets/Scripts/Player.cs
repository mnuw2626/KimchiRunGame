using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce; // 점프 수치

    [Header("References")]
    public Rigidbody2D PlayerRigidBody; //플레이어 몸
    public Animator PlayerAnimator; // 플레이어 애니메이션

    public BoxCollider2D PlayerCollider;

    private bool isGrounded = true; // 바닥에 있는지 여부

    private bool isInvincible = false; // 무적 여부

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // 플레이어가 바닥에 있고 스페이스바를 누를시
        {
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse); // 점프 동작 실행 - JumpForce 만큼 수직(Y축)에 Impulse(순간적인 힘)타입의 Force(힘)을 줌
            isGrounded = false; // 바닥에 없음
            PlayerAnimator.SetInteger("state", 1); // 점프 애니메이션 동작 실행
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
        GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1); //함수 이용 -> 인자 두개 중 제일 작은 값을 사용
    }

    void StartInvincible() 
    {
        isInvincible = true;
        Invoke("StopInvincible", 10f); // 10초 후에 StopInvincible() 호출
    }

    void StopInvincible()
    {
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D collision) //어디에 충돌하였는지 알수있는 함수
    {
        if (collision.gameObject.name == "Platform") // 바닥에 충돌함
        {
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2); // 착지 애니메이션 동작 실행
            }
            isGrounded = true; // 바닥에 충돌하면 바닥에 있음
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "enemy")
        {
            if (!isInvincible) { // 무적이 아닐 시
                Destroy(collider.gameObject); // (Player와 부딫힌 상대 오브젝트를)사라지게 함
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
