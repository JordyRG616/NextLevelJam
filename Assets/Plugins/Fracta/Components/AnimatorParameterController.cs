using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorParameterController : MonoBehaviour
{
    [SerializeField] private string parameterName;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Set(bool value)
    {
        animator.SetBool(parameterName, value);
    }
}
