using UnityEngine;
using System.Collections;

public interface IDamageable
{
    float Health { get; }
    void TakeDamage(float damageAmount);
    void RecoverHealth(float recoveryAmount);
}
