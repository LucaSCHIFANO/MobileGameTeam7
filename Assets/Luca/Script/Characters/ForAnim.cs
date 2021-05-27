using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAnim : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sr;

    private Rigidbody2D rb;
    private PlayerMovement pm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (pm.panelToGo != null)
        {
            if (pm.panelToGo.transform.position.x > transform.position.x)
            {
                sr.flipX = false;
                anim.SetBool("Idle", false);
            }
            else if (pm.panelToGo.transform.position.x < transform.position.x)
            {
                sr.flipX = true;
                anim.SetBool("Idle", false);
            }
            else
            {
                anim.SetBool("Idle", true);
            }

            if (pm.panelToGo.transform.position.y > transform.position.y)
            {
                anim.SetFloat("Dos", 1);
                anim.SetBool("Idle", false);
            }
            else if (pm.panelToGo.transform.position.y < transform.position.y)
            {
                anim.SetFloat("Dos", 0);
                anim.SetBool("Idle", false);
            }
            else
            {
                anim.SetBool("Idle", true);
            }
        }
    }
}
