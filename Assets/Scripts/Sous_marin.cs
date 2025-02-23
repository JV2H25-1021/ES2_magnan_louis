using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
public class Sous_marin : MonoBehaviour
{
    // Variable controle des mouvements
    [SerializeField] private float _vitessePromenade;

    private Rigidbody _rb;
    private Vector3 directionInput;
    // Variable controle des animations
    [SerializeField] private float _modifierAnimTranslation;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void OnPromener(InputValue directionBase) 
    {
        //deplacement du sous-marin
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitessePromenade;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);
    }
    // Press shitf vitesse double
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
        //Gestion des animation
        Vector3 vitesseSurPlane = new Vector3(0f, _rb.velocity.y, _rb.velocity.z);
        //Animation en Z
        _animator.SetFloat("VitesseZ", vitesseSurPlane.z * _modifierAnimTranslation);
        _animator.SetFloat("DeplacementZ", vitesseSurPlane.z);
        //Animation en Y
        _animator.SetFloat("VitesseY", vitesseSurPlane.y * _modifierAnimTranslation);
        _animator.SetFloat("DeplacementY", vitesseSurPlane.y);
    }
}