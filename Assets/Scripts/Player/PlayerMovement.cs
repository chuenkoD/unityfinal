using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
    
        [SerializeField] private float _horizontalSpeed;

        private Rigidbody2D _rigidbody;
        [SerializeField] private bool _faceRight;

        [Header("Jump")] [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;

        private bool _isJumping;
        private float _startJumpVerticalPosition;
    
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        } 
        private void Update() 
        {
            if(_isJumping)
                UpdateJump();
        
        } 
        public void MoveHorizontally(float direction)
        {
            if(_isJumping)
                return;
            
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            if(_isJumping)
                return;
            _isJumping = true;
            _rigidbody.AddForce(Vector2.up * _jumpForce);
            _rigidbody.gravityScale = _gravityScale;
            _startJumpVerticalPosition = transform.position.y;
        }

        private void SetDirection(float direction)
        {
            if ((_faceRight && direction < 0) ||
                (!_faceRight && direction > 0))
                Flip();
                
        }

        private void Flip()
        {
            transform.Rotate(0,180,0);
            _faceRight = !_faceRight;
        }

        private void UpdateJump()
        {
            if (_rigidbody.velocity.y < 0 && _rigidbody.position.y <= _startJumpVerticalPosition)
            {
                ResetJump();
                return;
            }
        }

        private void ResetJump()
        {
            _isJumping = false;
            _rigidbody.position = new Vector2(_rigidbody.position.x, _startJumpVerticalPosition);
            _rigidbody.gravityScale = 0;
        }
    }
}
