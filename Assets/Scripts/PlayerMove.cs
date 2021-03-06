using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    //중력 변수
    public float gravity = -20.0f;

    //점프력
    public float jumpPower = 4.0f;

    //최대 점프 횟수
    public int maxJump = 6;

    //현재 점프 횟수
    int jumpCount = 0;

    //수직 속도 변수
    float yVelocity = 0;

    //속력 변수
    public float moveSpeed = 7.0f;

    //나의 캐릭터 콘트롤러
    CharacterController cc;

    //체력 변수
    int hp;


    //최대 체력
    public int maxHp = 10;

    // 슬라이더 UI
    public Slider hpSlider;

    //회전 감도
    public float rotSpeed = 200.0f;

    // 이펙트 UI 오브젝트
    public GameObject hitEffect;


    // Start is called before the first frame update
    void Start()
    {
        //캐릭터 컨트롤러 컴포넌트 받기
        cc = GetComponent<CharacterController>();

        //체력변수 초기화
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수 종료
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //이동방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        //이동 방향(월드 좌표)을 카메라의 방향 기준으로 (로컬 좌표) 전환
        //dir = Camera.main.transform.TransformDirection(dir);

        if(!(h==0&&v==0))
        {
            //이동방향으로 플레이어 이동
            //P = P0 + VT
            //transform.position += dir * moveSpeed * Time.deltaTime;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
            

        }

        //플레이어가 땅에 착지시 현재 점프 횟수를 0으로 초기화
        //수직 속도 값(중력)을 다시 0으로 초기화
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        //점프 키를 누를 시, 점프력을 수직 속도로 적용
        //단, 현재 점프 횟수가 최대 점프 횟수를 넘어가지 않아야 함
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            jumpCount++;
            yVelocity = jumpPower;
        }        

        //캐릭터의 수직속도(중력)을 적용
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;


        // 슬라이더 value를 체력 비율로 적용
        hpSlider.value = (float)hp / (float)maxHp;

        //이동방향으로 플레이어 이동
        //P = P0 + VT
        //transform.position += dir * moveSpeed * Time.deltaTime;
        //cc.Move(dir * moveSpeed * Time.deltaTime);

    }



    // 플레이어 피격 함수
    public void DamageAction(int damage)
    {
        // 에너미의 공격력만큼 플레이어의 체력 감소
        hp -= damage;
        if(hp < 0)
        {
            hp = 0;
        }
        // hp가 0보다 큰 경우에는 화면이 붉어지는 이펙트 코루틴을 실행
        else
        {
            StartCoroutine(HitEffect());
        }
    }

    IEnumerator HitEffect()
    {
        // 1. 이펙트를 켠다(활성화 시킨다)
        hitEffect.SetActive(true);

        // 2. 0.3초 기다린다
        yield return new WaitForSeconds(0.3f);

        // 3. 이펙트를 끈다(비활성화)
        hitEffect.SetActive(false);
    }
}
