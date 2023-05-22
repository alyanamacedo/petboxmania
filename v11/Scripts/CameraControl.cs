using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] //permite que variáveis privadas sejam visíveis no editor
    private InputAction action; //spacebar no keyboard para alterar a câmera
    private Animator animator;
    private bool initCam = true;

    private void Awake() {
        animator = GetComponent<Animator>();
        animator.Play("VCam0");
    }

    private void OnEnable() {
        action.Enable();
    }
    private void OnDisable() {
        action.Disable();
    }

    private void Start() {
        action.performed += _ => SwitchState();

        StartCoroutine(IsStarting());
    }

    private void SwitchState(){
        if(initCam){
            animator.Play("VCam1");
        }else{
            animator.Play("VCam0");
        }
        initCam = !initCam;
    }

    IEnumerator IsStarting()
    {
        yield return new WaitForSeconds(3);
        animator.Play("VCam1");
    }
}