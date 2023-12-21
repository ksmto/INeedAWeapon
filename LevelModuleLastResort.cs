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
                    if (referances != null)
                    {
                        if (referances.name == "TurretSpawnPosition")
                        {
                            foreach (var turretSpawnPositionTransform in referances.transforms)
                            {
                                if (turretSpawnPositionTransform != null)
                                {
                                    if (turretSpawnPositionTransform.name == "TurretSpawnPosition")
                                    {
                                        Catalog.GetData<ItemData>("INAW.MapObjects.Turret").SpawnAsync(turret =>
                                        {
                                            turret.gameObject.TryGetOrAddComponent(out TurretBehaviour component);
                                            turret.disallowDespawn = true;
                                            turret.physicBody.isKinematic = true;
                                            turret.physicBody.useGravity = false;
                                            turret.physicBody.rigidBody.isKinematic = true;
                                            turret.physicBody.rigidBody.useGravity = false;
                                        }, turretSpawnPositionTransform.position, Quaternion.LookRotation(-Vector3.right));
                                    }
                                }
                            }
                        }

                        if (referances.name == "WindTurbine")
                        {
                            foreach (var windTurbineTransform in referances.transforms)
                            {
                                if (windTurbineTransform != null && windTurbineTransform.name == "WindTurbine")
                                {
                                    windTurbineTransform.gameObject.TryGetOrAddComponent(out WindTurbineBehaviour component);
                                }
                            }
                        }
                    }
                }
            }
            return base.OnLoadCoroutine();
        }
    }
}