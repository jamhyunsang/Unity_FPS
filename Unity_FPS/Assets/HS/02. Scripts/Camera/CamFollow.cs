using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //카메라가 플레이어를 따라다닌다.
    //플레이어한테 바로 카메라를 붙여서 이동해도 상관없다.
    //하지만 게임에 따라서 드라마틱한 연출이 필요한 경우에
    //타겟을 따라다니도록하는게 1인칭에서 3인칭으로 또는 그반대로 변경이 쉽다.
    //또한 순간이동이 아닌 슈팅게임에서 꼬랑지가 따라다니는것 같은 효과도 연출 가능하다.
    //지금은 우리 눈역활을 할거라서 그냥 순간이동 시기킨다.

    //타겟 지정
    //public Transform target;
    public float followSpeed = 5.0f;

    public Transform target1st; 
    public Transform target3rd;

    
    public bool isFPS = true;

    void Update()
    {
        //카메라 위치를 강제로 타겟위치에 고정해 둔다.
        //transform.position = target.position ;
        if(isFPS)
        {
            transform.position = target1st.position;
        }
        else
        {
            transform.position = target3rd.position;
        }
        //1인칭 to 3인칭 3인친 to 1인칭
        ChangeView();
        //FollowTarget();
    }

    private void ChangeView()
    {
        //isFPS의 값을바꾼다.
        if(Input.GetKeyDown("1"))
        {
            isFPS = true;
        }
        if (Input.GetKeyDown("3"))
        {
            isFPS = false;
        }
    }

    private void FollowTarget()
    {
        //타겟 방향 구하기 (벡터의 뺄셈)
        //방향 = 타켓 = 자기자신
        //Vector3 dir = target.position - transform.position;
        //dir.Normalize();
        //transform.Translate(dir * followSpeed * Time.deltaTime);

        //문제점 : 타겟에 도착하면 덜덜덜 떨림

        //if(Vector3.Distance(transform.position,target.position)<1.0f)
        //{
        //    transform.position = target.position;
        //}

        //1번을 누르면 카메라가 뒤로 10만큼 이동
        //2번을 누르면 타겟의 
        //if(!cameraSwitch&&Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    cameraSwitch = true;
        //}
        //else if(cameraSwitch&&Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    cameraSwitch = false;
        //}
    }

}
