using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject fxFactory;
    //폭탄의 역활
    //예쩐 총알은 생성하면 지 스스로 날아가다 충돌하면 터졌다.
    //하지만 폭탄은 생성되자마자 스스로 이동하면 될까? 안될까?
    //폭탄은 플레이어가 직접 던져야 한다.
    //폭탄이 다른 오브젝트들과 충돌하면 터져야 한다.

    private void OnCollisionEnter(Collision collision)
    {
        //폭발 이펙트 보여주기
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
        //이펙트 오브젝트가 사라지지 않는 경우
        //Destroy(fx,2.0f);
        //다른오브젝트도 삭제 시키기
        if (collision.collider.name != "Ground") Destroy(collision.gameObject);
        //자기자신 파괴 시키기(제일 마지막에 삭제 해야된다.)
        Destroy(gameObject);
    }
}
