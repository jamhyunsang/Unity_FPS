using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    //몬스터 상태 이넘문
    public enum EnemyState
    {
        Idle,Move,Attack,Return,Damaged,Die
    }
    //몬스터 상태 변수
    public EnemyState state;
    /// <summary>
    /// 유용한 기능
    /// </summary>

    //Player가져올 변수
    public GameObject target;
    //Enemy의 Player인식 거리
    public float targetDistance = 20.0f;
    //Enemy의 공격 범위
    public float attackDistacne = 1.0f;
    //Enemy가 추적할수 있는 거리 
    public float chaserDistance = 30.0f;
    //Enemy 이동 속도
    public float speed = 5.0f;
    //Enemy가 Player를 발견한 위치
    Vector3 findPlayer;
    //Enemy Hp
    public int hp = 100;
    //Enemy의 CharacterController 컴포넌트
    CharacterController charCtrl;

    //플레이어의 총알발사 스크립트 가져오기
    PlayerFire playerFire;
    #region Idle에 필요한 변수들
    #endregion
    #region Move에 필요한 변수들
    #endregion
    #region Attack에 필요한 변수들
    //총알 발사 타이밍
    private float curTime = 0.0f;
    //총알 발사 간격
    private float fireTime = 1.0f;

    #endregion
    #region Return에 필요한 변수들
    #endregion
    #region Damaged에 필요한 변수들
    #endregion
    #region Die에 필요한 변수들
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //몬스터 상태 초기화
        state = EnemyState.Idle;
        charCtrl = GetComponent<CharacterController>();
        playerFire = target.GetComponent<PlayerFire>();
    }

    // Update is called once per frame
    void Update()
    {
        //상태의 따른 행동 처리
        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        //1.플레이어와 일정범위가 되면 이동 상태로 변경 ( 탐지 범위)
        //-플레이어 찾기 (Gameobject.Find("Player"))
        //-일정거리 20미터 ( 거리 비교 : Distance, magnitude)
        //-상태 변경
        //state = EnemyState.Move;
        //-상태 전환 출력
        if (targetDistance > Vector3.Distance(transform.position, target.transform.position))
        {
            findPlayer = transform.position;
            state = EnemyState.Move;
        }

    }

    private void Move()
    {
        //1.플레이어를 향해 이동후 공격 범위 안에 들어오면 공격상태 변경
        //2. 플레이어를 추격하더라도 처음 위치에서 일정 범위를 넘어가면 리턴 상태로 변경
        //3.플레이어처럼 캐릭터 컨트롤러를 이용하기 
        //-공격 범위 2미터 
        //-상태 변경 
        //-상태 전환 출력
        transform.LookAt(target.transform);
        charCtrl.Move(transform.forward * speed * Time.deltaTime);
        if (chaserDistance < Vector3.Distance(transform.position, findPlayer))
        {
            state = EnemyState.Return;
        }
        if(attackDistacne>Vector3.Distance(transform.position, target.transform.position))
        {
            state = EnemyState.Attack;
        }

    }

    private void Attack()
    {
        //1.플레이어가 공격 범위 안에 있따면 일정한 시간 간격으로 플레이어 공격
        //2.플레이어가 공격 범위를 벗어나면 이동상태 (재추격)
        //3.공격 범위 2미터 
        //4.상태 변경
        //-상태전환 출력

        if (attackDistacne > Vector3.Distance(transform.position, target.transform.position))
        {
            curTime += Time.deltaTime;
            if (fireTime <= curTime)
            {
                curTime = 0.0f;
                Debug.Log("Attack");
            }
        }
        else
        {
            curTime = 0.0f;
            state = EnemyState.Move;
        }
    }
    private void Return()
    {
        //1. 몬스터가 플레이어를 추격하더라도 처음위치에서 일정 범위를 벗어나면다시 돌아옴
        //- 처음위치에서 일정범위 30미터
        //-상태 변경
        //-상태 전환출력
        transform.LookAt(findPlayer);

        if(transform.position!=findPlayer)
        {
            charCtrl.Move(transform.forward * speed * Time.deltaTime);
        }
        else
        {
         
            state = EnemyState.Idle;
        }
    }

    private void Damaged()
    {
        //코루틴을 사용하자
        //1.몬스터 체력 1이상
        //2. 다시 이전상태로 변경
        //-상태 변경
        //-상태 전환 출력
        
    }

    //wnr
    private void Die()
    {
        //코루틴을 사용하자
        //1. 체력이 0이하
        //2. 몬스터 오브젝트 삭제
        //- 상태 변경
        //- 상태 전환 출력 (죽었다.)
        Destroy(gameObject);
    }
}
