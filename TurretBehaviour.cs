using System;
using System.Collections;
using ThunderRoad;
using UnityEngine;
using Random = UnityEngine.Random;

namespace INeedAWeapon
{
    public class TurretBehaviour : ThunderBehaviour
    {
        private Item turret;
        private Animator animator;
        private Holder magazineHolder;
        private Vector3 bulletSpawnPosition;
        private Vector3 casingSpawnPosition;

        private readonly int maximumAmmunition = 200;
        private int currentAmmunition = 0;

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

                turret.OnHeldActionEvent += Turret_OnHeldActionEvent;
                magazineHolder.Snapped += MagazineHolder_Snapped;
                magazineHolder.UnSnapped += MagazineHolder_UnSnapped;
            }
        }

        private void Turret_OnHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            try
            {
                GameManager.local.StartCoroutine(ShootRoutine(action));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void MagazineHolder_Snapped(Item item)
        {
            currentAmmunition = maximumAmmunition;
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

            DisplayMessage.instance.ShowMessage(new DisplayMessage.MessageData($"Ammo: {currentAmmunition}", "", "", "", 1, 0, null, null, false, true, false, true, MessageAnchorType.Head, null, false, 1, null, true, null));
        }

        private IEnumerator ShootRoutine(Interactable.Action action)
        {
            var bulletItemData = Catalog.GetData<ItemData>("INAW.Ammunition.5.56x45mm.Bullet");
            var casingItemData = Catalog.GetData<ItemData>("INAW.Ammunition.5.56x45mm.Casing");

            Debug.Log("TRIED SHOOT");

            while (action == Interactable.Action.UseStart)
            {
                if (currentAmmunition > 0)
                {
                    currentAmmunition--;
                    DisplayMessage.instance.ShowMessage(new DisplayMessage.MessageData($"Ammo: {currentAmmunition}", "", "", "", 1, 0, null, null, false, true, false, true, MessageAnchorType.Head, null, false, 1, null, true));

                    Debug.Log("TRIED ANIMATION");
                    GameManager.local.StartCoroutine(BarrelSpinAnimationRoutine(action));

                    Debug.Log("TRIED BULLET/CASING SPAWN");
                    if (bulletItemData != null && casingItemData != null)
                    {
                        bulletItemData.SpawnAsync(bullet =>
                        {
                            bullet.Throw();
                            bullet.physicBody.AddForce(-turret.transform.right * Random.Range(60.0f, 75.0f), ForceMode.VelocityChange);
                            bullet.Despawn(8.0f);
                        }, bulletSpawnPosition, Quaternion.LookRotation(Vector3.Cross(casingSpawnPosition, Vector3.up)));

                        casingItemData.SpawnAsync(casing =>
                        {
                            casing.physicBody.AddForce((turret.transform.forward + turret.transform.up) * Random.Range(0.25f, 0.4f), ForceMode.Impulse);
                            casing.Despawn(5.0f);
                        }, casingSpawnPosition, Quaternion.LookRotation(Vector3.Cross(casingSpawnPosition, Vector3.up)));
                    }

                    foreach (var handler in turret.handlers)
                    {
                        handler?.HapticTick(1.0f);
                    }
                }
                else
                {
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(0.05f);
        }

        private IEnumerator BarrelSpinAnimationRoutine(Interactable.Action action)
        {
            if (action != Interactable.Action.UseStart)
            {
                animator.StopPlayback();
                yield break;
            }

            if (currentAmmunition > 0)
            {
                animator.Play("Spin");
                yield return new WaitForSeconds(0.5f);
                animator.StopPlayback();
                animator.Play("RapidSpin");
            }
            else
            {
                animator.StopPlayback();
                yield break;
            }
        }
    }
}