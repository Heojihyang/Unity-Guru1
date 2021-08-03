using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeController : MonoBehaviour
{
    public Image Panel;

    float time = 0f;
    public float F_time = 1000f;

    public void Fade()
    {
        StartCoroutine(FadeFlow());
        
    }
    IEnumerator FadeFlow()
    {
        yield return new WaitForSeconds(1.5f);

        Panel.gameObject.SetActive(true);
        //time = 0;
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        takeScene();
        /*
        time = 0;
       
        while (alpha.a>0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        */



    }

    // �� ȣ�� �Լ�
    void takeScene()
    {
        if (GameObject.Find("Player").GetComponent<PlayerController>().hp <= 0)
        {
            SceneManager.LoadScene("BadEnding"); // ��忣���� �ֱ�
        }
        
       
        if(GameObject.Find("Vaccine Score").GetComponent<ScoringSystem>().end_count==10) // ���ǿ����� ����
        {
            SceneManager.LoadScene("HappyScene"); // ���ǿ����� �ֱ�
        }
     
    }
    
    

    // Update is called once per frame
    void Update()
    {

    }
}

    