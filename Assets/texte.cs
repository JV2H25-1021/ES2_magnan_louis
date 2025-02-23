using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class texte : MonoBehaviour
{
    [SerializeField] private float _vitessePromenade;
    [SerializeField] private float _vitesseDouble = 2f; // Vitesse multipliée par 2 lors de la pression de Shift
    
    private Rigidbody _rb;
    private Vector3 directionInput;

    [SerializeField] private float _modifierAnimTranslation;
    private Animator _animator;
    private float _rotationVelocity;

    private bool _isShiftPressed = false; // Flag pour vérifier si Shift est pressé

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Fonction appelée par Input Action pour gérer le mouvement
    void OnPromener(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitessePromenade;
        
        // Si Shift est pressé, on double la vitesse
        if (_isShiftPressed)
        {
            directionAvecVitesse *= _vitesseDouble;
        }

        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);

        // Détecter la direction de l'entrée
        if (directionAvecVitesse.y > 0)
        {
            _animator.SetTrigger("Avancer");
            _animator.ResetTrigger("Idle");
        }
        else if (directionAvecVitesse.y < 0)
        {
            _animator.SetTrigger("Reculer");
            _animator.ResetTrigger("Idle");
        }

        if (directionAvecVitesse.x > 0)
        {
            _animator.SetTrigger("Droite");
            _animator.ResetTrigger("Idle");
        }
        else if (directionAvecVitesse.x < 0)
        {
            _animator.SetTrigger("Gauche");
            _animator.ResetTrigger("Idle");
        }

        // Si aucune direction n'est appuyée (nulle), revenir à l'animation de base
        if (directionAvecVitesse == Vector2.zero)
        {
            _animator.SetTrigger("Idle");
        }
    }

    // Cette fonction sera appelée lorsque Shift est pressé ou relâché
    public void OnShiftPressed(InputValue value)
    {
        // Vérifie si Shift est pressé ou relâché
        _isShiftPressed = value.isPressed;
    }

    void FixedUpdate()
    {
        Vector3 mouvement = directionInput;
        _rb.AddForce(mouvement, ForceMode.VelocityChange);
    }
}
