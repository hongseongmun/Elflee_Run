using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayerDie()
    {
        anim.SetBool("isPlayerDie", true);
        anim.SetFloat("EgleAnimSpeed", 0.0f);
    }

}
