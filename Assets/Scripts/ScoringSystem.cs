using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;
    public int end_count;

    private void Start()
    {
        theScore = 0;
        end_count = 0;
       
    }
    public void AddScore(int scoreValue)
    {
        theScore += scoreValue;
        end_count += scoreValue;
    }
    
    void Update()
    {

        // ��� ������ UI�� ���� 1����
        scoreText.GetComponent<Text>().text = "" + 1;
        
    

    }
    
}
