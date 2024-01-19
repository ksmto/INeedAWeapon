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
    public class FretModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<FretChip>();
        }
    }
    public class FretChip : ThunderBehaviour
    {


        private Item item;
        EffectInstance fret_SpawnGreet;
        EffectData fret_SpawnGreetEffect;
        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();
            fret_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWFretGreet");



            if (item != null)
            {
                item.OnSnapEvent += FretSnap;
                item.OnUnSnapEvent += FretUnSnap;
            }

        }

        private void FretUnSnap(Holder holder)
        {
            ModOptions.fretActive = false;
        }

        private void FretSnap(Holder holder)
        {
            FretGreeting();
            ModOptions.butlrActive = false;
            ModOptions.cortanaActive = false;
            ModOptions.circActive = false;
            ModOptions.fretActive = true;
            ModOptions.lumuActive = false;
            ModOptions.misterchiefActive = false;
            ModOptions.matiActive = false;
            ModOptions.vergilActive = false;
        }
        public void FretGreeting()
        {

                GameManager.local.StartCoroutine(FretGreetRoutine());
        }
        public IEnumerator FretGreetRoutine()
        {
            fret_SpawnGreet = fret_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            fret_SpawnGreet.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            yield return null;
        }
    }

    public class FretAnnouncer : HaloEvents
    {

        bool isPlaying;
        private EffectData fret_PlayerDeathEffect, fret_LowHPEffect, fret_EnemyWarnEffect, fret_KillEffect, fret_KillHeadshotEffect, fret_PlayerHealEffect;
        private EffectInstance fret_PlayerDeath, fret_LowHP, fret_EnemyWarn, fret_Kill, fret_Headshot, fret_PlayerHeal;
        public override void OnCatalogRefresh(EventTime eventTime)
        {
            fret_PlayerHealEffect = Catalog.GetData<EffectData>("INAWFretHeal");
            fret_PlayerDeathEffect = Catalog.GetData<EffectData>("INAWFretDeath");
            fret_LowHPEffect = Catalog.GetData<EffectData>("INAWFretLowHp");
            fret_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWFretEnemyWarn");
            fret_KillEffect = Catalog.GetData<EffectData>("INAWFretKill");
            fret_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWFretKillHeadshot");
        }


        /* EVENTS */

        public override void OnCreatureHeal(Creature creature, float heal, Creature healer, EventTime eventTime)
        {
            if (creature.isPlayer)
            {
                if (ModOptions.fretActive && eventTime == EventTime.OnStart)
                {
                    if (AiRandom())
                    {
                        FretPlayerHeal();
                    }
                }
            }

           
        }
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (ModOptions.fretActive && eventTime == EventTime.OnStart)
            {
                if (creature.isPlayer)
                {
                    if (AiRandom())
                    {
                        {
                            FretPlayerHurt();
                        }
                    }
                }
            }
        }
        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            {
                if (ModOptions.fretActive && eventTime == EventTime.OnStart)
                {
                    {
                        if (AiRandom())
                        {
                            FretKills(collisionInstance, creature);
                        }
                    }

                }

            }
        }
        public override void OnCreatureSpawn(Creature creature)
        {
            if (ModOptions.fretActive && !creature.isPlayer)
            {
                if (AiRandom())
                {

                    FretEnemyWarning();
                }

            }
        }

        /* METHODS */

        public void FretPlayerHeal()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(PlayerHealRoutine());
            }
        }

        public void FretPlayerHurt()
        {
            if (!isPlaying)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    GameManager.local.StartCoroutine(PlayerCriticalHurtRoutine());

                }
            }

        }
        public void FretKills(CollisionInstance collisionInstance, Creature creature)
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

        public void FretEnemyWarning()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(EnemySpawnWarningRoutine());
            }

        }

        public IEnumerator PlayerHealRoutine()
        {
            isPlaying = true;
            fret_PlayerHeal = fret_PlayerHealEffect?.Spawn(Player.local.creature.transform);
            fret_PlayerHeal.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator PlayerCriticalHurtRoutine()
        {
            isPlaying = true;
            fret_LowHP = fret_LowHPEffect?.Spawn(Player.local.creature.transform);
            fret_LowHP. Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator CreatureKillRoutine()
        {
            isPlaying = true;
            fret_Kill = fret_KillEffect?.Spawn(Player.local.creature.transform);
            fret_Kill.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator CreatureHeadShotRoutine()
        {
            isPlaying = true;
            fret_Headshot = fret_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
            fret_Headshot.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator PlayerKilledRoutine()
        {
            isPlaying = true;
            fret_PlayerDeath = fret_PlayerDeathEffect?.Spawn(Player.local.creature.transform);
            fret_PlayerDeath.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator EnemySpawnWarningRoutine()
        {
            isPlaying = true;
            fret_EnemyWarn = fret_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            fret_EnemyWarn.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
    }
}
