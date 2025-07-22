using System;
using System.Collections;
using Content.Features.AmmoSystem;
using Content.Features.BulletsPool;
using Content.Features.ConfigsSystem.Scripts;
using Content.Features.PlayerInput.Scripts;
using Core.EventBus;
using UnityEngine;
using Zenject;

namespace Content.Features.PlayerActions.Scripts
{
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        
        
        private IEventBus _eventBus;
        private IBulletPool _bulletPool;
        private IAmmoSystem _ammoSystem;
        private PlayerConfig _playerConfig;
        private float fireForce = 10f;
        private float fireCooldown = 0.25f;
        private float _currentTime = 0f;
        private bool canShoot;
        private bool autoFire;
        private bool isfacingRight;
        
        [Inject]
        public void Construct(IEventBus eventBus, IBulletPool bulletPool, IAmmoSystem _amSys, PlayerConfig playerC)
        {
            _eventBus = eventBus;
            _bulletPool = bulletPool;
            _ammoSystem = _amSys;
            _playerConfig = playerC;
            _eventBus.Subscribe<PlayerShootInputEvent>(OnShoot);
            _eventBus.Subscribe<PlayerShootInputReleasedEvent>(OnShootReleased);
            _eventBus.Subscribe<PlayerMoveRightInputEvent>(RotateRight);
            _eventBus.Subscribe<PlayerMoveLeftInputEvent>(RotateLeft);
            if (_playerConfig != null)
            {
                fireForce = _playerConfig.FireForce;
                fireCooldown = _playerConfig.FireCooldown;
            }
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
            _eventBus.Unsubscribe<PlayerMoveRightInputEvent>(RotateRight);
            _eventBus.Unsubscribe<PlayerMoveLeftInputEvent>(RotateLeft);
        }

        private void OnShoot(PlayerShootInputEvent _) => autoFire = true;

        private void OnShootReleased(PlayerShootInputReleasedEvent _) => autoFire = false;
        
        private void RotateLeft(PlayerMoveLeftInputEvent obj) => isfacingRight = false;

        private void RotateRight(PlayerMoveRightInputEvent obj) => isfacingRight = true;
        
        private void FireBullet()
        {
            if (_ammoSystem?.TryConsumeAmmo() is false)
            {
                Debug.Log("[PlayerShootingController] No ammo!");
                return;
            }

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