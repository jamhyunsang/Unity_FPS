using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{


    public GameObject bulletImpactFactory;
    public GameObject bombFactory;

    public GameObject firePos;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //발사 함수
        Fire();
    }

    //발사 함수
    private void Fire()
    {
        //마우스 왼쪽 버튼 클릭시 레이 캐스트로 총알 발사
        if(Input.GetMouseButtonDown(0))
        {
            //레이저
            Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
            //레이저 맞은 충돌
            RaycastHit hitInfo;
            if (Physics.Raycast(ray,out hitInfo))
            {
                //중돌 지점 총알 이펙트 생성
                GameObject bulletImpact = Instantiate(bulletImpactFactory);
                bulletImpact.transform.position = hitInfo.point;
                bulletImpact.transform.forward = hitInfo.normal;
            }
        }
        //마우스 우측 버튼 클릭시 수류탄 투척 하기
        if(Input.GetMouseButtonDown(1))
        {
           GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePos.transform.position;
            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            bomb.transform.rotation = Camera.main.transform.rotation;
            bomb.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 1000);
        }
    }
}
