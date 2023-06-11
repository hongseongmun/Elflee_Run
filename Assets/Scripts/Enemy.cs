using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;         // �̵� �ӵ�
    public float minX = -27f;            // �ּ� X ��ǥ
    public float maxX = -5f;             // �ִ� X ��ǥ
    public float respawnDelay = 2f;      // ����� ������

    private bool movingRight = true;     // ���������� �̵� ������ ����

    private void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * GameManager.hsmTimer);

            if (transform.position.x >= maxX)
            {
                movingRight = false;
                gameObject.SetActive(false);
                Invoke("Respawn", respawnDelay);
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * GameManager.hsmTimer);

            if (transform.position.x <= minX)
            {
                movingRight = true;
                gameObject.SetActive(false);
                Invoke("Respawn", respawnDelay);
            }
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
    }
}