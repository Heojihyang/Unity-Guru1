using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectVaccine : MonoBehaviour
{
    public AudioSource collectSound;
    void OnTriggerEnter(Collider other)
    {
        // ��� ������ ȿ���� ���鼭 UI�� ���� 1����
        collectSound.Play();
        ScoringSystem.theScore += 1;
        Destroy(gameObject);
    }
}
