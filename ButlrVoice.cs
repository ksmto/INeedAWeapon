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
    public class ButlrModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<ButlrChip>();
        }
    }
    public class ButlrChip : ThunderBehaviour
    {


        private Item item;
        EffectInstance Butlr_SpawnGreet;
        EffectData Butlr_SpawnGreetEffect;
        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();
            Butlr_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWButlrGreet");



            if (item != null)
            {
                item.OnSnapEvent += ButlrSnap;
                item.OnUnSnapEvent += ButlrUnSnap;
            }

        }

        private void ButlrUnSnap(Holder holder)
        {
            ModOptions.butlrActive = false;
        }

        private void ButlrSnap(Holder holder)
        {
            ButlrGreeting();
            ModOptions.butlrActive = true;
            ModOptions.cortanaActive = false;
            ModOptions.circActive = false;
            ModOptions.fretActive = false;
            ModOptions.lumuActive = false;
            ModOptions.misterchiefActive = false;
            ModOptions.matiActive = false;
            ModOptions.vergilActive = false;
        }
        public void ButlrGreeting()
        {

                GameManager.local.StartCoroutine(ButlrGreetRoutine());
        }
        public IEnumerator ButlrGreetRoutine()
        {
            Butlr_SpawnGreet = Butlr_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            Butlr_SpawnGreet.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            yield return null;
        }
    }

    public class ButlrAnnouncer : HaloEvents
    {

        bool isPlaying;
        private EffectData butlr_PlayerDeathEffect, butlr_LowHPEffect, butlr_EnemyWarnEffect, butlr_KillEffect, butlr_KillHeadshotEffect, butlr_PlayerHealEffect;
        private EffectInstance butlr_PlayerDeath, butlr_LowHP, butlr_EnemyWarn, butlr_Kill, butlr_Headshot, butlr_PlayerHeal;
        public override void OnCatalogRefresh(EventTime eventTime)
        {
            butlr_PlayerHealEffect = Catalog.GetData<EffectData>("INAWButlrHeal");
            butlr_PlayerDeathEffect = Catalog.GetData<EffectData>("INAWButlrDeath");
            butlr_LowHPEffect = Catalog.GetData<EffectData>("INAWButlrLowHp");
            butlr_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWButlrEnemyWarn");
            butlr_KillEffect = Catalog.GetData<EffectData>("INAWButlrKill");
            butlr_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWButlrKillHeadshot");
        }


        /* EVENTS */

        public override void OnCreatureHeal(Creature creature, float heal, Creature healer, EventTime eventTime)
        {
            if (creature.isPlayer)
            {
                if (ModOptions.butlrActive && eventTime == EventTime.OnStart)
                {
                    if (AiRandom())
                    {
                        ButlrPlayerHeal();
                    }
                }
            }

           
        }
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (ModOptions.butlrActive && eventTime == EventTime.OnStart)
            {
                if (creature.isPlayer)
                {
                    if (AiRandom())
                    {
                        {
                            ButlrPlayerHurt();
                        }
                    }
                }
            }
        }
        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            {
                if (ModOptions.butlrActive && eventTime == EventTime.OnStart)
                {
                    {
                        if (AiRandom())
                        {
                            ButlrKills(collisionInstance, creature);
                        }
                    }

                }

            }
        }
        public override void OnCreatureSpawn(Creature creature)
        {
            if (ModOptions.butlrActive && !creature.isPlayer)
            {
                if (AiRandom())
                {

                    ButlrEnemyWarning();
                }

            }
        }

        /* METHODS */

        public void ButlrPlayerHeal()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(PlayerHealRoutine());
            }
        }

        public void ButlrPlayerHurt()
        {
            if (!isPlaying)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    GameManager.local.StartCoroutine(PlayerCriticalHurtRoutine());

                }
            }

        }
        public void ButlrKills(CollisionInstance collisionInstance, Creature creature)
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

        public void ButlrEnemyWarning()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(EnemySpawnWarningRoutine());
            }

        }

        public IEnumerator PlayerHealRoutine()
        {
            isPlaying = true;
            butlr_PlayerHeal = butlr_PlayerHealEffect?.Spawn(Player.local.creature.transform);
            butlr_PlayerHeal.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator PlayerCriticalHurtRoutine()
        {
            isPlaying = true;
            butlr_LowHP = butlr_LowHPEffect?.Spawn(Player.local.creature.transform);
            butlr_LowHP. Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator CreatureKillRoutine()
        {
            isPlaying = true;
            butlr_Kill = butlr_KillEffect?.Spawn(Player.local.creature.transform);
            butlr_Kill.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator CreatureHeadShotRoutine()
        {
            isPlaying = true;
            butlr_Headshot = butlr_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
            butlr_Headshot.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator PlayerKilledRoutine()
        {
            isPlaying = true;
            butlr_PlayerDeath = butlr_PlayerDeathEffect?.Spawn(Player.local.creature.transform);
            butlr_PlayerDeath.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator EnemySpawnWarningRoutine()
        {
            isPlaying = true;
            butlr_EnemyWarn = butlr_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            butlr_EnemyWarn.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
    }
}
