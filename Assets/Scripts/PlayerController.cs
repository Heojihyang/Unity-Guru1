using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    //�߷� ����
    public float gravity = -20.0f;

    //������
    public float jumpPower = 4.0f;

    //�ִ� ���� Ƚ��
    public int maxJump = 6;

    //���� ���� Ƚ��
    int jumpCount = 0;

    //���� �ӵ� ����
    float yVelocity = 0;

    //�ӷ� ����
    public float moveSpeed = 7.0f;

    //���� ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    //ü�� ����
    public int hp;

    //�ִ� ü��
    public int maxHp = 10;

    // �����̴� UI
    public Slider hpSlider;

    //ȸ�� ����
    public float rotSpeed = 200.0f;

    // ����Ʈ UI ������Ʈ
    public GameObject hitEffect;

    //�ִϸ��̼�������Ʈ
    Animator anim;

    //�ǰݽ� ����
    new AudioSource audio;
    public AudioClip audioDamaged;


    // Start is called before the first frame update
    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ �ޱ�
        cc = GetComponent<CharacterController>();

        //ü�º��� �ʱ�ȭ
        hp = maxHp;

        //�ڽĿ�����Ʈ�� �ִϸ��̼� ������Ʈ����������
        anim = GetComponentInChildren<Animator>();

        this.audio = GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //�̵����� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        //�̵����� ������ǥ ����
        dir = transform.TransformDirection(dir);

        //�÷��̾ ���� ������ ���� ���� Ƚ���� 0���� �ʱ�ȭ
        //���� �ӵ� ��(�߷�)�� �ٽ� 0���� �ʱ�ȭ
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        //���� Ű�� ���� ��, �������� ���� �ӵ��� ����
        //��, ���� ���� Ƚ���� �ִ� ���� Ƚ���� �Ѿ�� �ʾƾ� ��
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            jumpCount++;
            yVelocity = jumpPower;

        }

        //ĳ������ �����ӵ�(�߷�)�� ����
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //�̵��������� �÷��̾� �̵�
        //P = P0 + VT
        //transform.position += dir * moveSpeed * Time.deltaTime;
        Move();
        cc.Move(dir * moveSpeed * Time.deltaTime);

        LookAround();

        // �����̴� value�� ü�� ������ ����
        hpSlider.value = (float)hp / (float)maxHp;
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 moveInput = new Vector2(h, v);
        bool isMove = moveInput.magnitude != 0;
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            //transform.position += moveDir * Time.deltaTime * 5f;

            //move�ִϸ��̼� ����
            anim.SetBool("isMove", isMove);

        }
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x<180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    // �÷��̾� �ǰ� �Լ�
    public void DamageAction(int damage)
    {
        // ���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ����
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
            anim.SetTrigger("isDie");
        }
        // hp�� 0���� ū ��쿡�� ȭ���� �Ӿ����� ����Ʈ �ڷ�ƾ�� ����
        else
        {
            StartCoroutine(HitEffect());
            anim.SetTrigger("isHitten");
            audio.Play();
        }
    }

    IEnumerator HitEffect()
    {
        // 1. ����Ʈ�� �Ҵ�(Ȱ��ȭ ��Ų��)
        hitEffect.SetActive(true);

        // 2. 0.3�� ��ٸ���
        yield return new WaitForSeconds(0.3f);

        // 3. ����Ʈ�� ����(��Ȱ��ȭ)
        hitEffect.SetActive(false);
    }
}
