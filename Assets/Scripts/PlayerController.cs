using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Vector3 coliderSize; // 콜라이더의 크기

    public float MoveSpeed;
    public float jumpForce;
    public float doubleJumpFoce;

    public bool jumping = true;
    public bool doubleJumping = false;
    public Vector3 velocity;
    public UnityEvent onHit;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayerMovement()
    {
        if (GameManager.isPlay)
        {
            //좌우 이동하는 부분
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < 8.0f)
                {
                    transform.Translate(new Vector2(MoveSpeed * Time.deltaTime, 0));
                }
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (transform.position.x > -8.0f)
                {
                    transform.Translate(new Vector2(-MoveSpeed * Time.deltaTime, 0));
                }
            }

            // 점프에 대한 부분
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("isJumping", true);

                if (jumping)
                {
                    velocity.y = jumpForce;
                    jumping = false;
                    doubleJumping = false;
                }
                else if (!doubleJumping)
                {
                    velocity.y = doubleJumpFoce;
                    doubleJumping = true;
                }
            }

            if (transform.position.y < -3.2f)
            {
                anim.SetBool("isJumping", false);
                transform.position = new Vector3(transform.position.x, -2.68f, transform.position.z);
                jumping = true;
                doubleJumping = false;
                velocity.y = 0.0f;
            }
        }
    }
    public void HsmGravity()
    {
        // 중력에 대한 부분
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (GameManager.isPlay)
            {
                if (transform.position.y > -3.2f)
                {
                    velocity += Physics.gravity * 6 * Time.deltaTime;
                }
            }
        }
        else
        {
            if (transform.position.y > -3.2f)
            {
                velocity += Physics.gravity * Time.deltaTime;
            }
        }
        transform.position += velocity * Time.deltaTime;
    }
    //콜라이더 구현
    public void HsmColider()
    {
        // 현재 게임 오브젝트의 충돌 영역 계산
        Bounds bounds = new Bounds(transform.position, coliderSize);

        // 충돌 감지를 위해 주변에 있는 게임 오브젝트들을 가져옴
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        // 주변에 있는 게임 오브젝트들을 순회하며 처리
        foreach (GameObject obj in objects)
        {
            // 현재 게임 오브젝트와 같은 객체는 제외
            if (obj == gameObject)
                continue;

            // 다른 게임 오브젝트의 위치 정보를 가져와 충돌 검사
            Vector3 otherPosition = obj.transform.position;

            if (bounds.Contains(otherPosition))
            {
                if (obj.gameObject.CompareTag("Ground"))
                {
                    jumping = true;
                    doubleJumping = false;
                    velocity.y = 0f;
                }
                if (obj.gameObject.CompareTag("Impediments"))
                {
                    anim.SetTrigger("doDie");
                    onHit.Invoke();
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        // 에디터 상에서 박스 콜라이더의 크기를 시각적으로 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, coliderSize * 0.5f);
    }
}