using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScoller : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollAmount;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private  Vector3 moveDirection;

    public void Scoller()
    {
        if (GameManager.isPlay || GameManager.isTitle)
        {
            transform.position += moveDirection * (moveSpeed * GameManager.globalSpeed) * GameManager.hsmTimer;
            if (transform.position.x <= -scrollAmount)
            {
                transform.position = target.position - moveDirection * scrollAmount;
            }
        }
    }
}