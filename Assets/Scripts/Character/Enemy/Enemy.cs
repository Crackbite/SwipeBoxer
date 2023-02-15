using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(EnemyAnimator), typeof(Power))]
public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private float _impactForce = 43f;
    [SerializeField] private float _hideSecAfterKill = 3f;
    [SerializeField] private Material _deathMaterial;
    [SerializeField] private float _destroySecAfterHide = 2f;
    [SerializeField] private PowerCanvas _powerCanvas;

    private EnemyAnimator _animator;
    private Collider[] _childrenColliders;
    private Power _power;

    public event UnityAction Died;
    public event UnityAction<Vector3> PreDied;
    public event UnityAction<Vector3> Won;

    public bool Dead { get; private set; }

    private void Start()
    {
        _childrenColliders = GetComponentsInChildren<Collider>();
        _power = GetComponent<Power>();
        _animator = GetComponent<EnemyAnimator>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HitArea hitArea) == false)
        {
            return;
        }

        if (Dead)
        {
            IgnoreCollisionWith(collision.collider);
            return;
        }

        if (CanTakeHit(hitArea) == false)
        {
            return;
        }

        var strikerPower = hitArea.GetComponentInParent<Power>();

        if (strikerPower == null)
        {
            return;
        }

        IgnoreCollisionWith(collision.collider);

        if (strikerPower.Current < _power.Current)
        {
            if (strikerPower.TryGetComponent(out Player striker))
            {
                Bounds colliderBounds = GetComponent<Collider>().bounds;
                Won?.Invoke(colliderBounds.center);

                _animator.RunIdleToVictory();
                striker.Die(_power);

                return;
            }
        }

        Vector3 hitPosition = collision.GetContact(collision.contactCount - 1).point;
        PreDied?.Invoke(hitPosition);

        Dead = true;
        Died?.Invoke();

        Destroy(_powerCanvas.gameObject);

        strikerPower.Increase();

        ChangeBodyToDead();

        TakeHit(collision);

        StartCoroutine(HideBody());
    }

    private bool CanTakeHit(HitArea hitArea)
    {
        const float RaycastRaise = 0.2f;
        const int RaycastHitBuffer = 128;

        Vector3 position = transform.position;
        position.y += RaycastRaise;

        var raycastHits = new RaycastHit[RaycastHitBuffer];
        float distance = Vector3.Distance(position, hitArea.transform.position);
        Vector3 direction = hitArea.transform.forward * -1;

        Physics.RaycastNonAlloc(position, direction, raycastHits, distance);

        foreach (RaycastHit raycastHit in raycastHits)
        {
            Collider raycastCollider = raycastHit.collider;

            if (raycastCollider == null)
            {
                continue;
            }

            if (raycastCollider.TryGetComponent(out InactiveRoof _))
            {
                return false;
            }

            if (raycastCollider.TryGetComponent(out Enemy enemy))
            {
                if (enemy.Dead == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void ChangeBodyToDead()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = _deathMaterial;
        EnemyEmotion[] emotions = GetComponentsInChildren<EnemyEmotion>();

        foreach (EnemyEmotion emotion in emotions)
        {
            if (emotion.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.enabled = spriteRenderer.enabled == false;
            }
        }
    }

    private IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(_destroySecAfterHide);
        Destroy(gameObject);
    }

    private IEnumerator HideBody()
    {
        yield return new WaitForSeconds(_hideSecAfterKill);

        foreach (Collider currentCollider in _childrenColliders)
        {
            currentCollider.enabled = false;
        }

        StartCoroutine(DestroyBody());
    }

    private void IgnoreCollisionWith(Collider ignoredCollider)
    {
        foreach (Collider currentCollider in _childrenColliders)
        {
            Physics.IgnoreCollision(currentCollider, ignoredCollider);
        }
    }

    private void TakeHit(Collision collision)
    {
        Quaternion hitQuaternion = collision.transform.rotation;
        var player = collision.gameObject.GetComponentInParent<Player>();

        if (player != null)
        {
            hitQuaternion = player.transform.rotation;
        }

        Vector3 hitDirection = (hitQuaternion * Vector3.forward) + Vector3.up;
        var hipsBone = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        hipsBone.AddForce(hitDirection * _impactForce, ForceMode.VelocityChange);
    }
}