using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _blood;
    [SerializeField] private GameObject _bullet;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _bulletDamage;

    public void Init(float speed, int damage)
    {
        _bulletSpeed = speed;
        _bulletDamage = damage;

        MoveBullet(transform);
        GameEvents.OnGameRestart += ResetBullet;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameRestart -= ResetBullet;
    }
    private void MoveBullet(Transform bullet)
    {
        bullet.DOMove(bullet.position + bullet.forward * _bulletSpeed, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            MoveBullet(bullet);
        });
    }
    private void OnTriggerEnter(Collider other)
    {
        var damageableObject = other.GetComponentInParent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.GetDamage(_bulletDamage);
            transform.DOKill();
            _bullet.SetActive(false);
            _blood.Play();
            Destroy(gameObject, _blood.duration);
        }
    }

    private void ResetBullet()
    {
        Destroy(gameObject);
    }
}
