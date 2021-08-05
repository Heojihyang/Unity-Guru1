using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    // ���� ���� ���
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // �� ���� ����
    EnemyState e_state;

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

    // �� ���ݷ�
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

        // �ʱ� �� ���´� idle
        e_state = EnemyState.Idle;

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
        switch (e_state)
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
                break;
            case EnemyState.Die:
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
            e_state = EnemyState.Move;

            anim.SetTrigger("IdletoMove");
        }
    }

    void Move()
    {

        // ���� ��ġ�� �̵� ���� ������
        if (Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            // ���� ���¸� ���ͷ� ��ȯ
            e_state = EnemyState.Return;            
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
            e_state = EnemyState.Attack;            

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
                print("���� ����!");
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
            e_state = EnemyState.Move;            

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

        // �ʱ� ��ġ������ �Ÿ��� 0.9f �ʰ��� �ʱ� ��ġ ������ �̵�
        //if (Vector3.Distance(transform.position, originPos) > 1.0f)
        if (dist.magnitude > 0.9f)
        {            
            //Vector3 dir = (originPos - transform.position).normalized;
            Vector3 dir = dist.normalized;
            smith.SetDestination(originPos);
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
            e_state = EnemyState.Idle;          
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
        // anim.SetTrigger("Damaged");
        // �ǰ� ��Ǹ�ŭ ��ٸ���
        yield return new WaitForSeconds(0.5f);

        // ���� ���¸� �̵� ���·� ��ȯ
        e_state = EnemyState.Move;        
    }

    void Die()
    {
        // �������� �ǰ� �ڷ�ƾ ����
        StopAllCoroutines();

        // ���� ���� ó�� �ڷ�ƾ ����
        StartCoroutine(DieProcess());
        
        anim.SetTrigger("Die");

        VaccineManager.instance.DropVaccineToPosition(transform.position, 1);

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
    public void HitEnemy(int value)
    {        
        // �̹� �ǰ�, ���, ���� ���¶�� �Լ� ����
        if (e_state == EnemyState.Damaged || e_state == EnemyState.Die || e_state == EnemyState.Return)
        {
            return;
        }

        // �÷��̾��� ���ݷ¸�ŭ ���ʹ� ü�� ����
       currentHp -= value;

        // ���� hp�� 0���� ũ�� 
        if (currentHp > 0)
        {
            //�ǰ� ���·� ��ȯ
            e_state = EnemyState.Damaged;           
            Damaged();
        }
        // �׷��� �ʴٸ�
        else
        {
            //���� ���·� ��ȯ
            e_state = EnemyState.Die;            
            Die();
        }
    }
}