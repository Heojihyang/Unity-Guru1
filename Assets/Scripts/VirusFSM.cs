using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VirusFSM : MonoBehaviour
{
    // ���� ���� ���
    enum VirusState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // ���� ���� ����
    VirusState vr_state;

    //�÷��̾� ���� ������Ʈ
    GameObject player;

    // �÷��̾� �߰� ����
    public float findDistance = 8.0f;

    // ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // �̵��ӵ�
    public float moveSpeed = 5.0f;

    // ���� ���� ����
    public float attackDistance = 2.0f;

    // �����ð�
    float currentTime = 0;

    // ���� ������ �ð�
    float attackDelay = 2.0f;

    // ���� ���ݷ�
    public int attackPower = 3;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos;
    Quaternion originRot;

    // �̵� ���� ����
    public float moveDistance = 20.0f;

    // �ִ� ü��
    public int maxHp = 10;

    //���� ü�� ����
    int currentHp;

    // �����̴� ����
    public Slider hpSlider;

    //�׺���̼� �޽� ������Ʈ
    NavMeshAgent smith;

    //�ִϸ����� ������Ʈ ����
    Animator anim;



    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ �ޱ�
        cc = GetComponent<CharacterController>();

        // �ʱ� ���� ���´� idle
        vr_state = VirusState.Idle;

        // �÷��̾� �˻�
        player = GameObject.Find("Player");

        // �ʱ� ��ġ, ȸ�� ����
        originPos = transform.position;
        originRot = transform.rotation;

        //���� ü�� ����
        currentHp = maxHp;

        //�׺�޽� ������Ʈ ������Ʈ ��������
        smith = GetComponent<NavMeshAgent>();
        smith.speed = moveSpeed;
        smith.stoppingDistance = attackDistance;

        //�ڽĿ�����Ʈ�� �ִϸ��̼� ������Ʈ����������
        anim = GetComponentInChildren<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // ���� ���� üũ >> �ش� ���º��� ������ ��� ����
        switch (vr_state)
        {
            case VirusState.Idle:
                Idle();
                break;
            case VirusState.Move:
                Move();
                break;
            case VirusState.Attack:
                Attack();
                break;
            case VirusState.Return:
                Return();
                break;
            case VirusState.Damaged:
                break;
            case VirusState.Die:
                break;

        }

        // hp �����̴� ���� ü�� ���� ���� 
        hpSlider.value = (float)currentHp / (float)maxHp;
    }

    void Idle()
    {
        // �÷��̾���� �Ÿ��� �߰� �����̳� >> move
        if (Vector3.Distance(player.transform.position, transform.position) < findDistance)
        {
            //�̵� ���·� ����
            vr_state = VirusState.Move;
            print("Villain ������ȯ: Idle -> Move");

            anim.SetTrigger("IdletoMove");
        }
    }

    void Move()
    {

        // ���� ��ġ�� �̵� ���� ������
        if (Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            // ���� ���¸� ���ͷ� ��ȯ
            vr_state = VirusState.Return;
            print("Villain ���� ��ȯ: Move -> Return");
        }

        // �÷��̾���� �Ÿ��� ���� �������� �� ��� >> �÷��̾ ���� �̵�
        else if (Vector3.Distance(player.transform.position, transform.position) > attackDistance)
        {
            //�̵����� ���Ѵ�
            Vector3 dir = (player.transform.position - transform.position).normalized;

            //���� ���� ������ �̵������ ��ġ��Ų��
            transform.forward = dir;

            //�׺�޽� ������Ʈ�� �̿��Ͽ� Ÿ�� �������� �̵�
            smith.SetDestination(player.transform.position);
            smith.stoppingDistance = attackDistance;
        }

        // ���� ���� ��
        else
        {
            //���� ���·� ����
            vr_state = VirusState.Attack;
            print("Villain ���� ��ȯ: Move -> Attack");

            //�ִϸ��̼� ȣ��
            anim.SetTrigger("MovetoAttackDelay");

            // ���� �ð��� ���� ������ �ð���ŭ �̸� ����
            currentTime = attackDelay;

            //�̵� ���߰�, Ÿ�� �ʱ�ȭ
            smith.isStopped = true;
            smith.ResetPath();
        }
    }

    //����
    void Attack()
    {
        // �÷��̾ ���� ���� ���� ���� >> �÷��̾� ����
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            //���� ���ð��� ���� ��� �ð��� �Ѿ�ٸ�
            if (currentTime >= attackDelay)
            {
                currentTime = 0;
                //�÷��̾� ����
                print("Virus�� ����!");
                HitEvent();

                anim.SetTrigger("StartAttack");


            }

            //// ������ �ð����� �÷��̾� ����
            //currentTime += Time.deltaTime;
            //if (currentTime > attackDelay)
            //{
            //    player.GetComponent<PlayerController>().DamageAction(attackPower);
            //    currentTime = 0;
            //}
            else
            {
                //�ð��� ����
                currentTime += Time.deltaTime;
            }
        }
        else
        {
            //�̵� ���·� ��ȯ
            vr_state = VirusState.Move;
            print("Virus ���� ��ȯ: Attack -> Move");

            anim.SetTrigger("AttacktoMove");
        }
    }

    //�÷��̾�� �������� �ִ� �Լ�
    public void HitEvent()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.DamageAction(attackPower);
    }

    void Return()
    {

        Vector3 dist = originPos - transform.position;
        dist.y = 0;

        // �ʱ� ��ġ������ �Ÿ��� 0.1f �ʰ��� �ʱ� ��ġ ������ �̵�
        //if (Vector3.Distance(transform.position, originPos) > 1.0f)
        if (dist.magnitude > 0.9f)
        {
            //print("return");
            //Vector3 dir = (originPos - transform.position).normalized;
            Vector3 dir = dist.normalized;
            Vector3 tar = dir * moveSpeed * Time.deltaTime;
            smith.SetDestination(tar);
            // ������ ���� �������� ��ȯ
            transform.forward = dir;

        }
        // �׷��� �ʴٸ� �ڽ��� ��ġ�� �ʱ� ��ġ�� ���� >> ��� ���� ��ȯ
        else
        {
            // ��ġ, ȸ�� ���� �ʱ� ���·� ��ȯ
            transform.position = originPos;
            transform.rotation = originRot;

            //��� ���·� ��ȯ
            vr_state = VirusState.Idle;
            print("Villain ���� ��ȯ: Return -> Idle");
        }
    }

    void Damaged()
    {
        // �ǰ� ���� ó�� �ڷ�ƾ ����
        StartCoroutine(DamageProcess());

    }

    // ������ ó���� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        print("virus");
        anim.SetTrigger("Damaged");
        // �ǰ� ��Ǹ�ŭ ��ٸ���
        yield return new WaitForSeconds(0.5f);

        // ���� ���¸� �̵� ���·� ��ȯ
        vr_state = VirusState.Move;
    }

    void Die()
    {
        // �������� �ǰ� �ڷ�ƾ ����
        StopAllCoroutines();

        // ���� ���� ó�� �ڷ�ƾ ����
        StartCoroutine(DieProcess());

        anim.SetTrigger("Die");



    }

    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ ��Ȱ��ȭ
        cc.enabled = false;

        // 2�ʵ��� ��ٸ� �� �ڱ� �ڽ� ����
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);



    }

    // ������ ó�� �Լ�
    public void HitVillain(int value)
    {
        // �̹� �ǰ�, ���, ���� ���¶�� �Լ� ����
        if (vr_state == VirusState.Damaged || vr_state == VirusState.Die || vr_state == VirusState.Return)
        {
            return;
        }

        // �÷��̾��� ���ݷ¸�ŭ ���ʹ� ü�� ����
        currentHp -= value;

        // ���� hp�� 0���� ũ�� 
        if (currentHp > 0)
        {
            //�ǰ� ���·� ��ȯ
            vr_state = VirusState.Damaged;
            print("Villain ���� ��ȯ: Any state -> Damaged");
            Damaged();
        }
        // �׷��� �ʴٸ�
        else
        {
            //���� ���·� ��ȯ
            vr_state = VirusState.Die;
            print("Villain ���� ��ȯ: Any state -> Die");
            Die();
        }
    }
}
