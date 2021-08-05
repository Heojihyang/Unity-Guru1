using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{   
    //�߻��� ��ġ
    public Transform fireTransform;

    // �ǰ� ����Ʈ ���� ������Ʈ
    public GameObject bulletEffect;

    // ��ƼŬ �ý��� ����
    ParticleSystem ps;

    // �ҵ��� �߻� ȿ�� ����Ʈ 
    public ParticleSystem sanitizerEffect;

    // ���� ���ݷ�
    public int weaponPower = 5;

    //�����Ÿ�
    private float fireDistance = 50f;

    //public interface IDamageable
    //{
    //    void TakeHit(float damage, RaycastHit hit);
    //}

    void Start()
    {
        // ��ƼŬ �ý��� ������Ʈ ��������
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    public void Shot()
    {
        //���� ����
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //�浹 ���� ����
        RaycastHit hit;

        //�Ѿ��� ���� �� ����
        Vector3 hitPosition = Vector3.zero;

        //����ĳ��Ʈ
        if (Physics.Raycast(ray, out hit))
        {
            string layerName = LayerMask.LayerToName(hit.transform.gameObject.layer);

            //�ε��� ����� �̸��� �ܼ�â�� ���
            print(layerName);
            
            //���̰� �浹�� ���

            ////�浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
            //IDamageable target = hit.collider.GetComponent<IDamageable>();

            //���̿� �ε��� ����� ���̾ 'Enemy'�� ��� ������ �Լ� ����
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {         
                EnemyFSM eFSM = hit.transform.GetComponent<EnemyFSM>();
                //HitEnemy �Լ� ����
                eFSM.HitEnemy(weaponPower);
            }

            // �ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵�
            bulletEffect.transform.position = hit.point;

            // �ǰ� ����Ʈ�� forward  ������ ���̰� �ε��� ������ ��� ���Ϳ� ��ġ
            bulletEffect.transform.forward = hit.normal;
            

            ////�� ���� ����
            //if (target != null)
            //{
            //    target.TakeHit(weaponPower, hit);
            //}

            //���� �浹 ��ġ ����
            hitPosition = hit.point;
        }
        else
        {
            //�浹���� �ʾҴٸ�
            //�ִ� �����Ÿ����� ���� ���� ��ġ�� �浹 ��ġ�� ���
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        //�߻� ����Ʈ ���        
        sanitizerEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
        //// ���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� ����
        //// ���콺 ���� ��ư �Է�
        if (Input.GetMouseButton(0))
        {
            //    // ���� ���� �� �߻�� ��ġ, ���� ���� ����
            //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //    // ���̰� �ε��� ����� ������ ������ ���� ����
            //    RaycastHit hitInfo = new RaycastHit();

            //    // ���� �߻� �� ���� �ε��� ��ü�� ������ �ǰ� ����Ʈ ǥ��
            //    if (Physics.Raycast(ray, out hitInfo))
            //    {
            //        

            //        // �ǰ� ����Ʈ�� �÷���
            //        ps.Play();
            Shot();
        }
    }
}
