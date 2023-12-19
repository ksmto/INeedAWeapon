using System;
using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon
{
    public class LevelModuleLastResort : LevelModule
    {
        private Transform turretSpawnPosition;

        public override IEnumerator OnLoadCoroutine()
        {
            try
            {
                if (level != null)
                {
                    Debug.Log("LOADED");

                    foreach (var customReferances in level.customReferences)
                    {
                        if (customReferances != null && customReferances.name == "TurretSpawnPosition")
                        {
                            foreach (var transforms in customReferances.transforms)
                            {
                                if (transforms != null)
                                {
                                    if (transforms.gameObject.name == "TurretSpawnPosition")
                                    {
                                        Debug.Log("GET TURRET SPAWN POS");
                                        turretSpawnPosition = transforms;
                                        EventManager.onLevelLoad += EventManager_onLevelLoad;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return base.OnLoadCoroutine();
        }

        private void EventManager_onLevelLoad(LevelData levelData, EventTime eventTime)
        {
            if (eventTime == EventTime.OnEnd)
            {
                Catalog.GetData<ItemData>("INAW.MapObjects.Turret").SpawnAsync(turret =>
                {
                    turret.gameObject.AddComponent<TurretBehaviour>();
                    turret.disallowDespawn = true;
                    turret.physicBody.isKinematic = true;
                    turret.physicBody.useGravity = false;
                    turret.transform.position = turretSpawnPosition.position;
                    turret.transform.rotation = Quaternion.LookRotation(Vector3.forward);
                });
            }
        }
    }
}