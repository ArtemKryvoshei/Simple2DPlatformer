using System;
using System.Collections;
using Content.Features.BulletsPool;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.PlayerActions.Scripts
{
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireForce = 10f;
        [SerializeField] private float fireCooldown = 0.25f;

        private IEventBus _eventBus;
        private IBulletPool _bulletPool;
        private float _currentTime = 0f;
        private bool canShoot;
        private bool autoFire;
        private bool isfacingRight;
        
        [Inject]
        public void Construct(IEventBus eventBus, IBulletPool bulletPool)
        {
            _eventBus = eventBus;
            _bulletPool = bulletPool;
            _eventBus.Subscribe<PlayerShootInputEvent>(OnShoot);
            _eventBus.Subscribe<PlayerShootInputReleasedEvent>(OnShootReleased);
            _eventBus.Subscribe<PlayerMoveRightInputEvent>(RotateRight);
            _eventBus.Subscribe<PlayerMoveLeftInputEvent>(RotateLeft);
        }

        

        private void Update()
        {
            if (!canShoot)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= fireCooldown)
                {
                    canShoot = true;
                    _currentTime = 0f;
                }
            }
        }

        private void FixedUpdate()
        {
            if (autoFire && canShoot)
            {
                FireBullet();
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerShootInputEvent>(OnShoot);
            _eventBus.Unsubscribe<PlayerShootInputReleasedEvent>(OnShootReleased);
        }

        private void OnShoot(PlayerShootInputEvent _) => autoFire = true;

        private void OnShootReleased(PlayerShootInputReleasedEvent _) => autoFire = false;
        
        private void RotateLeft(PlayerMoveLeftInputEvent obj) => isfacingRight = false;

        private void RotateRight(PlayerMoveRightInputEvent obj) => isfacingRight = true;
        
        private void FireBullet()
        {
            canShoot = false;
            GameObject bulletGO = _bulletPool.Rent();
            if (bulletGO == null) return;

            bulletGO.transform.position = firePoint.position;
            bulletGO.transform.rotation = Quaternion.identity;

            var bullet = bulletGO.GetComponent<BulletController>();
            if (bullet != null)
            {
                bullet.Initialize(_bulletPool);
                if (isfacingRight)
                {
                    bullet.Fire(Vector2.right, fireForce);
                }
                else if (!isfacingRight)
                {
                    bullet.Fire(Vector2.left, fireForce);
                }
            }
        }
    }
}