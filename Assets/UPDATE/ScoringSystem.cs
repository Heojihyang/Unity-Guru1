using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;

    private void Start()
    {
        theScore = 0;
    }

    void Update()
    {
        // ��� ������ UI�� ���� 1����
        scoreText.GetComponent<Text>().text = "" + theScore;

    }
}
