using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //이동 속도
    public float speed = 5.0f;

    //캐릭터 컴포넌트 변수
    private CharacterController character;


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
        character.Move(dir*speed*Time.deltaTime);
    }
}
