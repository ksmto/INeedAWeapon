using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using Random = System.Random;


namespace INeedAWeapon
{
    public class CortanaModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<CortanaChip>();
        }
    }

    public class CortanaChip : ThunderBehaviour
    {


        private Item item;
        CortanaAnnouncer announcer;
        EffectInstance Cortana_SpawnGreet;
        EffectData Cortana_SpawnGreetEffect;
        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();
            Cortana_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWCortanaGreet");



            if (item != null)
            {
                item.OnSnapEvent += CortanaSnap;
                item.OnUnSnapEvent += CortanaUnSnap;
            }

        }

        private void CortanaUnSnap(Holder holder)
        {
            CortanaAnnouncer.cortanaActive = false;
        }

        private void CortanaSnap(Holder holder)
        {
            CortanaGreeting();
            CortanaAnnouncer.cortanaActive = true;
        }
        public void CortanaGreeting()
        {

                GameManager.local.StartCoroutine(CortanaGreetRoutine());
        }
        public IEnumerator CortanaGreetRoutine()
        {
            Cortana_SpawnGreet = Cortana_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            Cortana_SpawnGreet.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            yield return null;
        }
    }

    public class CortanaAnnouncer : HaloEvents
    {
        public static bool cortanaActive;
        int intQuoteRoll;
        Random random = new Random();
        bool isPlaying;
        private EffectData Cortana_DeathEffect, Cortana_LowHPEffect, Cortana_EnemyWarnEffect, Cortana_KillEffect, Cortana_KillHeadshotEffect;
        private EffectInstance CortanaPlayerDeath, Cortana_LowHP, CortanaEnemyWarn, Cortana_Kill, Cortana_Headshot;
        public override void OnCatalogRefresh(EventTime eventTime)
        {
            Cortana_DeathEffect = Catalog.GetData<EffectData>("INAWCortanaDeath");
            Cortana_LowHPEffect = Catalog.GetData<EffectData>("INAWCortanaLowHp");
            Cortana_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWCortanaEnemyWarn");
            Cortana_KillEffect = Catalog.GetData<EffectData>("INAWCortanaKill");
            Cortana_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWCortanaKillHeadshot");
        }


        /* EVENTS */
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (cortanaActive && eventTime == EventTime.OnStart)
            {

                if (CortanaRandom())
                {
                    
                    if (creature.isPlayer)
                    {
                        Debug.Log("Cortana Player hit triggered");
                        CortanaPlayerHurt();
                    }
                }

            }
        }
        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            {
                if (cortanaActive && eventTime == EventTime.OnStart)
                {
                    {
                        if (CortanaRandom())
                        {
                            Debug.Log("cortana creature kill triggered");
                            CortanaKills(collisionInstance, creature);
                        }
                    }

                }

            }
        }
        public override void OnCreatureSpawn(Creature creature)
        {
            if (cortanaActive && !creature.isPlayer)
            {
                if (CortanaRandom())
                {

                    CortanaEnemyWarning();
                }

            }
        }

        /* METHODS */
        public bool CortanaRandom()
        {
            intQuoteRoll = random.Next(100 + 1);
            Debug.Log("Cortana Random roll = " + intQuoteRoll);
            if (ModOptions.quoteChance <= intQuoteRoll)
            {
                return true;
            }
            else return false;
        }


        public void CortanaPlayerHurt()
        {
            if (!isPlaying)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    GameManager.local.StartCoroutine(PlayerCriticalHurtRoutine());

                }
            }

        }
        public void CortanaKills(CollisionInstance collisionInstance, Creature creature)
        {
            if (!isPlaying)
            {
                if (creature.isPlayer)
                {
                    GameManager.local.StartCoroutine(PlayerKilledRoutine());
                }

                else if (collisionInstance.damageStruct.hitRagdollPart == collisionInstance.damageStruct.hitRagdollPart.ragdoll.headPart)
                {
                    GameManager.local.StartCoroutine(CreatureHeadShotRoutine());
                }
                else
                {   
                    GameManager.local.StartCoroutine(CreatureKillRoutine());
                }
            }

        }

        public void CortanaEnemyWarning()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(EnemySpawnWarningRoutine());
            }

        }

        public IEnumerator PlayerCriticalHurtRoutine()
        {
            isPlaying = true;
            Cortana_LowHP = Cortana_LowHPEffect?.Spawn(Player.local.creature.transform);
            Cortana_LowHP. Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator CreatureKillRoutine()
        {
            isPlaying = true;
            Cortana_Kill = Cortana_KillEffect?.Spawn(Player.local.creature.transform);
            Cortana_Kill.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator CreatureHeadShotRoutine()
        {
            isPlaying = true;
            Cortana_Headshot = Cortana_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
            Cortana_Headshot.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator PlayerKilledRoutine()
        {
            isPlaying = true;
            CortanaPlayerDeath = Cortana_DeathEffect?.Spawn(Player.local.creature.transform);
            CortanaPlayerDeath.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator EnemySpawnWarningRoutine()
        {
            isPlaying = true;
            CortanaEnemyWarn = Cortana_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            CortanaEnemyWarn.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
    }
}
