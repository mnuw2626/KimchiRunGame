using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce; // 점프 수치

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;

    private bool isGrounded = true; // 바닥에 있는지 여부

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
        }

    }

    void OnCollisionEnter2D(Collision2D collision) //어디에 충돌하였는지 알수있는 함수
    {
        if (collision.gameObject.name == "Platform") // 바닥에 충돌함
        {
            isGrounded = true; // 바닥에 충돌하면 바닥에 있음
        }
    }
}
