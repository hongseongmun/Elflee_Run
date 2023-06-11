using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;         // 이동 속도
    public float minX = -27f;            // 최소 X 좌표
    public float maxX = -5f;             // 최대 X 좌표
    public float respawnDelay = 2f;      // 재생성 딜레이

    private bool movingRight = true;     // 오른쪽으로 이동 중인지 여부

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