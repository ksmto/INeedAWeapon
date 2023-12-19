using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class TurretBehaviour : ThunderBehaviour
    {
        private Item turret;
        private Animator animator;
        private Holder magazineHolder;
        private Vector3 bulletSpawnPosition;
        private Vector3 casingSpawnPosition;

        private int maximumAmmunition = 200;
        private int currentAmmunition = 0;

        private bool triggerHeld = false;

        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            turret = GetComponent<Item>();

            if (turret != null)
            {
                animator = turret.GetComponent<Animator>();
                magazineHolder = turret.GetCustomReference<Holder>("MagazineHolder");

                bulletSpawnPosition = turret.GetCustomReference<Transform>("BulletSpawnTrasform").position;
                casingSpawnPosition = turret.GetCustomReference<Transform>("CasingSpawnTrasform").position;

                if (magazineHolder != null)
                {
                    magazineHolder.Snapped += item =>
                    {
                        currentAmmunition = maximumAmmunition;
                    };

                    magazineHolder.UnSnapped += item =>
                    {
                        if (currentAmmunition > 0)
                        {
                            currentAmmunition = 1;
                        }
                        else
                        {
                            currentAmmunition = 0;
                        }
                    };
                }

                turret.OnHeldActionEvent += Turret_OnHeldActionEvent;
            }
        }

        private void Turret_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            if (action == Interactable.Action.UseStart)
            {
                triggerHeld = true;
                GameManager.local.StartCoroutine(ShootRoutine());
            }
            else if (action == Interactable.Action.UseStop)
            {
                triggerHeld = false;
            }
        }

        private void Shoot()
        {
            var bulletItemData = Catalog.GetData<ItemData>("UNSCBattleRifle_Fire");
            var casingItemData = Catalog.GetData<ItemData>("UNSCBattleRifle_Casing");

            if (currentAmmunition > 0)
            {
                if (bulletItemData != null && casingItemData != null)
                {
                    bulletItemData.SpawnAsync(bullet =>
                    {
                        bullet.Throw();
                        bullet.physicBody.AddForce(-turret.transform.right * UnityEngine.Random.Range(60.0f, 75.0f), ForceMode.VelocityChange);
                        bullet.Despawn(8.0f);
                    }, bulletSpawnPosition, Quaternion.LookRotation(-turret.transform.right));

                    casingItemData.SpawnAsync(casing =>
                    {
                        casing.physicBody.AddForce((turret.transform.forward + turret.transform.up) * UnityEngine.Random.Range(3.0f, 7.5f), ForceMode.Impulse);
                        casing.Despawn(5.0f);
                    }, casingSpawnPosition, Quaternion.LookRotation(-turret.transform.right));
                }

                if (animator != null)
                {
                    animator.Play("Spin");
                }
            }
        }

        private IEnumerator ShootRoutine()
        {
            while (triggerHeld)
            {
                Shoot();
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}