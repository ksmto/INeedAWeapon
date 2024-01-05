using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;
using Random = UnityEngine.Random;

namespace INeedAWeapon
{
    public class TurretBehaviour : MonoBehaviour
    {
        private Item turret;
        private Animator animator;
        private Holder magazineHolder;

        private ParticleSystem muzzleFlashEffect;
        private AudioSource fireSFX;
        private AudioSource dryFireSFX;
        private AudioSource magazineSFX;

        private Vector3 bulletSpawnPosition;
        private Vector3 casingSpawnPosition;

        private readonly int maximumAmmunition = 200;
        private int currentAmmunition = 0;

        private void Start()
        {
            turret = GetComponent<Item>();

            if (turret != null)
            {
                animator = turret.GetComponent<Animator>();
                magazineHolder = turret.GetCustomReference<Holder>("MagazineHolder");

                muzzleFlashEffect = turret.GetCustomReference<ParticleSystem>("MuzzleFlashEffect");
                fireSFX = turret.GetCustomReference<AudioSource>("FireSFX");
                dryFireSFX = turret.GetCustomReference<AudioSource>("DryFireSFX");
                magazineSFX = turret.GetCustomReference<AudioSource>("MagazineSFX");

                turret.OnHeldActionEvent += Turret_OnHeldActionEvent;
                if (magazineHolder != null)
                {
                    magazineHolder.Snapped += MagazineHolder_Snapped;
                    magazineHolder.UnSnapped += MagazineHolder_UnSnapped;
                }
            }
        }

        private void Update()
        {
            if (turret != null)
            {
                // Bullet
                var bulletSpawnTransform = turret.GetCustomReference<Transform>("BulletSpawnTrasform");
                bulletSpawnTransform.rotation = turret.transform.rotation;
                bulletSpawnPosition = bulletSpawnTransform.position;
                // Muzzle Flash
                muzzleFlashEffect.transform.position = bulletSpawnPosition;
                muzzleFlashEffect.transform.rotation = turret.transform.rotation;
                // Casing
                var casingSpawnTransform = turret.GetCustomReference<Transform>("CasingSpawnTrasform");
                casingSpawnTransform.rotation = turret.transform.rotation;
                casingSpawnPosition = casingSpawnTransform.position;
            }
        }

        private void MagazineHolder_Snapped(Item item)
        {
            currentAmmunition = maximumAmmunition;
            magazineSFX?.Play();
            DisplayMessage.instance.ShowMessage(new DisplayMessage.MessageData($"Ammo: {currentAmmunition}", "", "", "", 1, 0, null, null, false, true, false, true, MessageAnchorType.Head, null, false, 1, null, true, null));
        }

        private void MagazineHolder_UnSnapped(Item item)
        {
            if (currentAmmunition > 0)
            {
                currentAmmunition = 1;
            }
            else
            {
                currentAmmunition = 0;
            }
            magazineSFX?.Play();

            DisplayMessage.instance.ShowMessage(new DisplayMessage.MessageData($"Ammo: {currentAmmunition}", "", "", "", 1, 0, null, null, false, true, false, true, MessageAnchorType.Head, null, false, 1, null, true, null));
        }

        private void Turret_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            try
            {
                var bulletItemData = Catalog.GetData<ItemData>("INAW.Ammunition.5.56x45mm.Bullet");
                var casingItemData = Catalog.GetData<ItemData>("INAW.Ammunition.5.56x45mm.Casing");

                if (action == Interactable.Action.UseStart)
                {
                    Debug.Log("TRIED SHOOT");

                    animator?.SetBool("firing", true);

                    if (currentAmmunition > 0)
                    {
                        currentAmmunition--;
                        fireSFX?.Play();
                        muzzleFlashEffect?.Play();
                        DisplayMessage.instance.ShowMessage(new DisplayMessage.MessageData($"Ammo: {currentAmmunition}", "", "", "", 1, 0, null, null, false, true, false, true, MessageAnchorType.Head, null, false, 1, null, true));

                        Debug.Log("TRIED BULLET/CASING SPAWN");
                        if (bulletItemData != null && casingItemData != null)
                        {
                            bulletItemData.SpawnAsync(bullet =>
                            {
                                bullet.Throw();
                                bullet.physicBody.AddForce(-turret.transform.right * Random.Range(60.0f, 75.0f), ForceMode.VelocityChange);
                                /*if (explosiveBullets)
                                {
                                    bullet.mainCollisionHandler.OnCollisionStartEvent += instance =>
                                    {
                                        var explosionEffectData = Catalog.GetData<EffectData>("MeteorExplosion");
                                        if (explosionEffectData != null)
                                        {
                                            var explosionEffectInstance = explosionEffectData.Spawn(instance.contactPoint, Quaternion.identity);
                                            explosionEffectInstance?.Play();
                                        }

                                        foreach (var hitColliders in Physics.OverlapSphere(instance.contactPoint, 5.0f))
                                        {
                                            if (hitColliders.TryGetComponentInParent(out Creature creature))
                                            {
                                                if (!creature.isPlayer)
                                                {
                                                    foreach (var part in creature.ragdoll.parts)
                                                    {
                                                        if (part != null)
                                                        {
                                                            creature.ragdoll?.SetState(Ragdoll.State.Destabilized);
                                                            part.physicBody?.rigidBody?.AddExplosionForce(5.0f, instance.contactPoint, 5.0f, 1.0f, ForceMode.VelocityChange);
                                                        }
                                                    }
                                                    creature.Damage(new CollisionInstance(new DamageStruct(DamageType.Energy, 5.0f)));

                                                    if (creature.data.id != "Chicken")
                                                    {
                                                        creature.handLeft?.TryRelease();
                                                        creature.handRight?.TryRelease();
                                                    }
                                                }
                                            }
                                        }
                                        bullet.Despawn();
                                    };
                                }
                                bullet.Despawn(8.0f);*/
                            }, bulletSpawnPosition, Quaternion.LookRotation(-turret.transform.right));

                            casingItemData.SpawnAsync(casing =>
                            {
                                casing.physicBody.AddForce((turret.transform.forward + turret.transform.up) * 0.3f, ForceMode.Impulse);
                                casing.Despawn(10.0f);
                            }, casingSpawnPosition, Quaternion.LookRotation(-turret.transform.right));
                        }

                        foreach (var handler in turret.handlers)
                        {
                            handler?.HapticTick(1.0f);
                        }
                    }
                    else
                    {
                        dryFireSFX.Play();
                    }
                }
                else
                {
                    animator?.SetBool("firing", false);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}