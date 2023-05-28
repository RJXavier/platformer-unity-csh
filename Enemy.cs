using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator ANI;

    protected AudioSource DEATH;
    protected virtual void Start()
    {
        ANI = GetComponent<Animator>();

        DEATH = GetComponent<AudioSource>();
    }



    public void Crushed()
    {
        ANI.SetTrigger("IsDead");
        DEATH.Play();
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }

}
