using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectVaccine : MonoBehaviour
{
    
    public GameObject vaccine;
    public Transform parent;
    public AudioSource collectSound;
    public int score = 1;    
     

    private void Update()
    {
        
    }

    private void Function_Instantiate()
    {
        GameObject inst = Instantiate(vaccine, parent);
        
    }

    //IEnumerator dropTheItems()
    //{
    //    yield return new WaitForSeconds(0.3f);

    //    //���� �ڸ��� ��ȯ
    //    Instantiate(gameObject, transform.position, Quaternion.identity);
    //}

    void OnTriggerEnter(Collider other)
    {
        // ��� ������ ȿ���� ���鼭 UI�� ���� 1����
        collectSound.Play();
        other.gameObject.GetComponent<ScoringSystem>().AddScore(score);
        //Destroy(gameObject);
    }
}
