using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Vector3 coliderSize; // �ݶ��̴��� ũ��

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
            //�¿� �̵��ϴ� �κ�
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

            // ������ ���� �κ�
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
        // �߷¿� ���� �κ�
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
    //�ݶ��̴� ����
    public void HsmColider()
    {
        // ���� ���� ������Ʈ�� �浹 ���� ���
        Bounds bounds = new Bounds(transform.position, coliderSize);

        // �浹 ������ ���� �ֺ��� �ִ� ���� ������Ʈ���� ������
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        // �ֺ��� �ִ� ���� ������Ʈ���� ��ȸ�ϸ� ó��
        foreach (GameObject obj in objects)
        {
            // ���� ���� ������Ʈ�� ���� ��ü�� ����
            if (obj == gameObject)
                continue;

            // �ٸ� ���� ������Ʈ�� ��ġ ������ ������ �浹 �˻�
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
        // ������ �󿡼� �ڽ� �ݶ��̴��� ũ�⸦ �ð������� ǥ��
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, coliderSize * 0.5f);
    }
}