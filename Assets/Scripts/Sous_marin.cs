using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
public class Sous_marin : MonoBehaviour
{

    [SerializeField] private float _vitessePromenade;

    private Rigidbody _rb;
    private Vector3 directionInput;
    
    [SerializeField] private float _modifierAnimTranslation;
    private Animator _animator;
    private float _rotationVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void OnPromener(InputValue directionBase) 
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitessePromenade;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);
    }
    void OnShiftPressed(InputValue etatBouton)
    {
        if(etatBouton.isPressed)
        {
            _vitessePromenade *= 2;
        }
        else
        {
            _vitessePromenade /= 2;
        }
    }
    void FixedUpdate()
    {
        Vector3 mouvement = directionInput;
        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        Vector3 vitesseSurPlane = new Vector3(0f, _rb.velocity.y, _rb.velocity.z);

        _animator.SetFloat("VitesseZ", vitesseSurPlane.z * _modifierAnimTranslation);
        _animator.SetFloat("DeplacementZ", vitesseSurPlane.z);

        _animator.SetFloat("VitesseY", vitesseSurPlane.y * _modifierAnimTranslation);
        _animator.SetFloat("DeplacementY", vitesseSurPlane.y);
    }
}