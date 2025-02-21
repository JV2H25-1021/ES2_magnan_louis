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

    void OnMouvement(InputValue directionBase) 
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitessePromenade;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
