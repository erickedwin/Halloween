using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    //private PlayerMovement _playerMovement;

    private readonly int directionX = Animator.StringToHash("X");

    private readonly int directionY = Animator.StringToHash("Y");

    private readonly int IsDead = Animator.StringToHash("IsDead");

    private void Start()
    {
        _animator = GetComponent<Animator>();
       // _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        /*
         if (_playerMovement.isMoving)
        {
            _animator.SetFloat(directionX, _playerMovement.getDirection.x);
            _animator.SetFloat(directionY, _playerMovement.getDirection.y);
        }
         */


        UpdateLayer();
    }

    public void ChangeToDead()
    {
        _animator.SetBool(IsDead, true);
    }

    public void Resurrect()
    {
        _animator.SetBool(IsDead, false);
    }

    private void ActivateLayer(PlayerAnimatorLayers localLayer)
    {
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0);
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(localLayer.GetLayer()), 1);
    }

    private void UpdateLayer()
    {
        /*
         if (_playerMovement.isMoving)
        {
            ActivateLayer(PlayerAnimatorLayers.Walk);
        }
        else
        {
            ActivateLayer(PlayerAnimatorLayers.Idle);
        }
         */

    }
}