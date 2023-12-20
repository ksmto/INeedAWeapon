using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class LevelModuleLastResort : LevelModule
    {
        public override IEnumerator OnLoadCoroutine()
        {
            if (level != null)
            {
                foreach (var referances in level.customReferences)
                {
                    if (referances != null && referances.name == "TurretSpawnPosition")
                    {
                        foreach (var transform in referances.transforms)
                        {
                            if (transform != null && transform.name == "TurretSpawnPosition")
                            {
                                Catalog.GetData<ItemData>("INAW.MapObjects.Turret").SpawnAsync(turret =>
                                {
                                    turret.gameObject.AddComponent<TurretBehaviour>();
                                    turret.disallowDespawn = true;
                                    turret.physicBody.isKinematic = true;
                                    turret.physicBody.useGravity = false;
                                    turret.physicBody.rigidBody.isKinematic = true;
                                    turret.physicBody.rigidBody.useGravity = false;
                                }, transform.position, Quaternion.LookRotation(-Vector3.right));
                            }
                        }
                    }
                }
            }
            return base.OnLoadCoroutine();
        }
    }
}