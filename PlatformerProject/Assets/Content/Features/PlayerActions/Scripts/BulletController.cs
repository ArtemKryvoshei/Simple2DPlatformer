using Content.Features.BulletsPool;
using UnityEngine;

namespace Content.Features.PlayerActions.Scripts
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 3f;
        [SerializeField] private float speed = 10f;
        [SerializeField] private LayerMask collisionMask;

        private IBulletPool _pool;
        private Vector2 _direction;
        private float _lifeTimer;
        private bool _isActive;

        public void Initialize(IBulletPool pool)
        {
            _pool = pool;
        }

        public void Fire(Vector2 direction, float forceMultiplier = 1f)
        {
            _direction = direction.normalized * speed * forceMultiplier;
            _lifeTimer = lifeTime;
            _isActive = true;
            gameObject.SetActive(true);
        }

        private void FixedUpdate()
        {
            FlyCycle();
        }

        private void FlyCycle()
        {
            if (!_isActive) return;

            // Движение
            transform.position += (Vector3)(_direction * Time.deltaTime);

            // Проверка коллизии по маске
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, speed * Time.deltaTime, collisionMask);
            if (hit.collider != null)
            {
                ReturnToPool();
                return;
            }

            // Таймер жизни
            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer <= 0f)
            {
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            _isActive = false;
            gameObject.SetActive(false);
            _pool.Return(this.gameObject);
        }
    }
}