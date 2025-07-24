using UnityEngine;

namespace Content.Features.ConfigsSystem.Scripts
{
    [CreateAssetMenu(menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 7f;

        [Header("Shooting")]
        [SerializeField] private float fireCooldown = 0.2f;
        [SerializeField] private int maxAmmo = 10;
        [SerializeField] private float fireForce = 10f;
        
        public float MoveSpeed => moveSpeed;
        public float JumpForce => jumpForce;
        public float FireCooldown => fireCooldown;
        public int MaxAmmo => maxAmmo;
        public float FireForce => fireForce;
    }

}