﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject[] skillParticles;
    private IEnumerator coroutine;
    [SerializeField] Slider hpBar, manaBar;

    void Start()
    {
        animator = GetComponent<Animator>();

        hpBar.value = hpBar.maxValue;
        manaBar.value = manaBar.maxValue;

        for (int i = 0; i < skillParticles.Length; i++)
        {
            skillParticles[i].SetActive(false);
        }
    }

    void Update()
    {
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Sprint", true);
        }
        else
        {
            animator.SetBool("Sprint", false);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) UseSkill(1);
        if (Input.GetKeyUp(KeyCode.Alpha2)) UseSkill(2);

    }



    public void UseSkill(int skillNumber)
    {
        animator.SetTrigger("Skill" + skillNumber);
        skillParticles[skillNumber - 1].SetActive(true);

        switch (skillNumber)
        {
            case 1:
                gameObject.GetComponent<AudioSource>().Play();
                coroutine = WaitToEnableObject(skillParticles[skillNumber - 1], 5f);
                break;
            case 2:
                coroutine = WaitToEnableObject(skillParticles[skillNumber - 1], 2f);
                break;

        }

        StartCoroutine(coroutine);
    }

    IEnumerator WaitToEnableObject(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);

    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

}
