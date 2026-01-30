using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private List<MovementEnum> _inputSequence = new List<MovementEnum>();

    [SerializeField]
    SpriteRenderer spriteRenderer;

    public void HandleMovement(MovementEnum movementEnum)
    {
        _inputSequence.Add(movementEnum);
        if(TryGetComponent(out ColorRecognizer colorRecognizer))
        {
            if(colorRecognizer.GetColor(_inputSequence, out Color color))
            {
                spriteRenderer.color = color;
            }
        }

        transform.position += movementEnum.ToVector3() * _moveSpeed;
    }
}