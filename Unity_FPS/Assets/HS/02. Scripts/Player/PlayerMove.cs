using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //이동 속도
    public float speed = 5.0f;

    //캐릭터 컴포넌트 변수
    private CharacterController character;

    //중력
    public float gravity = -20;
    //낙하 속도 (방향과 힘을 들고 있다)
    float velocityY;

    float jumpPower = 10.0f;

    int jumpCount;

    void Start()
    {
        //캐릭터 컴포넌트 담기
        character = GetComponent<CharacterController>();
        

    }

    
    void Update()
    {
        //이동 함수
        Move();
    }

    //이동 함수
    private void Move()
    {
        //이동 방향 키 입력 받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        //대각선 이동 속도를 상하좌우 속도와 동일하게 만든다.
        //게임에 따라 일부로 대각선은 빠르게 이동하도록 하는 경우도 있다.
        //이럴때는 벡터의 정규화를 하지 않는다.
        //dir.Normalize();

        //이동하기
        //transform.Translate(dir*speed*Time.deltaTime);

        //카메라가 보는 방향으로 이동해야 한다.
        dir = Camera.main.transform.TransformDirection(dir);
        //transform.Translate(dir * speed * Time.deltaTime);

        //심각한 문제 : 하늘 날라다님, 땅뚫음, 충돌처리 안됨
        //character.Move(dir*speed*Time.deltaTime);

        
        

        //캐릭터 점프 
        //점프버튼을 누르면 수직 속도에 점프 파워를 넣는다.
        //땅에 닿을때 velocityY 0으로 초기화

        //땅에 닿았는지
        //if(character.isGrounded)
        //{
        //    velocityY = 0;
        //}


        //CollisionFlags.Above;머리부분
        //CollisionFlags.Below;다리부분
        //CollisionFlags.Sides;몸통부분

        //땅을 밟았을때
        if(character.collisionFlags==CollisionFlags.Below)
        {
            velocityY = 0;
            jumpCount = 0;
        }
        //땅을 밟지 않았을때
        else
        {
            velocityY += gravity * Time.deltaTime;
            dir.y = velocityY;
        }
        //점프키를 누를때
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpCount++;
            velocityY = jumpPower;
        }
        character.Move(dir * speed * Time.deltaTime);
    }
}
