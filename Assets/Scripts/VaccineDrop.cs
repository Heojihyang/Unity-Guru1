using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaccineDrop : MonoBehaviour
{
    public GameObject vaccine;

    

    //IEnumerator dropTheItems()
    //{
    //    yield return new WaitForSeconds(0.3f);

    //    //���� �ڸ��� ��ȯ
    //    Instantiate(vaccine, trans.position, Quaternion.identity);

    //    //�ı�
    //    Destroy(this.gameObject);
    //}

    // Start is called before the first frame update
    //void Start()
    //{
    //    // ���� ���� ó�� �ڷ�ƾ ����
    //    StartCoroutine(DieProcess());        
    //}

    //IEnumerator DieProcess()
    //{
    //    // ĳ���� ��Ʈ�ѷ� ������Ʈ ��Ȱ��ȭ
    //    cc.enabled = false;

    //    // 2�ʵ��� ��ٸ� �� �ڱ� �ڽ� ����
    //    yield return new WaitForSeconds(2f);
    //    Destroy(gameObject);
    //}

    // Update is called once per frame
    /*
    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "Vaccine")
        {
            Debug.Log("ȹ��");
            Destroy(vaccine);
        }       
    }
    */
}
