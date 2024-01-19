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
    public class LumuModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<LumuChip>();
        }
    }
    public class LumuChip : ThunderBehaviour
    {


        private Item item;
        EffectInstance lumu_SpawnGreet;
        EffectData lumu_SpawnGreetEffect;
        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();
            lumu_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWLumuGreet");



            if (item != null)
            {
                item.OnSnapEvent += LumuSnap;
                item.OnUnSnapEvent += LumuUnSnap;
            }

        }

        private void LumuUnSnap(Holder holder)
        {
            ModOptions.lumuActive = false;
        }

        private void LumuSnap(Holder holder)
        {
            LumuGreeting();
            ModOptions.butlrActive = false;
            ModOptions.cortanaActive = false;
            ModOptions.circActive = false;
            ModOptions.fretActive = false;
            ModOptions.lumuActive = true;
            ModOptions.misterchiefActive = false;
            ModOptions.matiActive = false;
            ModOptions.vergilActive = false;
        }
        public void LumuGreeting()
        {

                GameManager.local.StartCoroutine(LumuGreetRoutine());
        }
        public IEnumerator LumuGreetRoutine()
        {
            lumu_SpawnGreet = lumu_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            lumu_SpawnGreet.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            yield return null;
        }
    }

    public class LumuAnnouncer : HaloEvents
    {

        bool isPlaying;
        private EffectData lumu_PlayerDeathEffect, lumu_LowHPEffect, lumu_EnemyWarnEffect, lumu_KillEffect, lumu_KillHeadshotEffect, lumu_PlayerHealEffect;
        private EffectInstance lumu_PlayerDeath, lumu_LowHP, lumu_EnemyWarn, lumu_Kill, lumu_Headshot, lumu_PlayerHeal;
        public override void OnCatalogRefresh(EventTime eventTime)
        {
            lumu_PlayerHealEffect = Catalog.GetData<EffectData>("INAWLumuHeal");
            lumu_PlayerDeathEffect = Catalog.GetData<EffectData>("INAWLumuDeath");
            lumu_LowHPEffect = Catalog.GetData<EffectData>("INAWLumuLowHp");
            lumu_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWLumuEnemyWarn");
            lumu_KillEffect = Catalog.GetData<EffectData>("INAWLumuKill");
            lumu_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWLumuKillHeadshot");
        }


        /* EVENTS */

        public override void OnCreatureHeal(Creature creature, float heal, Creature healer, EventTime eventTime)
        {
            if (creature.isPlayer)
            {
                if (ModOptions.lumuActive && eventTime == EventTime.OnStart)
                {
                    if (AiRandom())
                    {
                        LumuPlayerHeal();
                    }
                }
            }

           
        }
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (ModOptions.lumuActive && eventTime == EventTime.OnStart)
            {
                if (creature.isPlayer)
                {
                    if (AiRandom())
                    {
                        {
                            LumuPlayerHurt();
                        }
                    }
                }
            }
        }
        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            {
                if (ModOptions.lumuActive && eventTime == EventTime.OnStart)
                {
                    {
                        if (AiRandom())
                        {
                            LumuKills(collisionInstance, creature);
                        }
                    }

                }

            }
        }
        public override void OnCreatureSpawn(Creature creature)
        {
            if (ModOptions.lumuActive && !creature.isPlayer)
            {
                if (AiRandom())
                {

                    LumuEnemyWarning();
                }

            }
        }

        /* METHODS */

        public void LumuPlayerHeal()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(PlayerHealRoutine());
            }
        }

        public void LumuPlayerHurt()
        {
            if (!isPlaying)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    GameManager.local.StartCoroutine(PlayerCriticalHurtRoutine());

                }
            }

        }
        public void LumuKills(CollisionInstance collisionInstance, Creature creature)
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

        public void LumuEnemyWarning()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(EnemySpawnWarningRoutine());
            }

        }

        public IEnumerator PlayerHealRoutine()
        {
            isPlaying = true;
            lumu_PlayerHeal = lumu_PlayerHealEffect?.Spawn(Player.local.creature.transform);
            lumu_PlayerHeal.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator PlayerCriticalHurtRoutine()
        {
            isPlaying = true;
            lumu_LowHP = lumu_LowHPEffect?.Spawn(Player.local.creature.transform);
            lumu_LowHP. Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator CreatureKillRoutine()
        {
            isPlaying = true;
            lumu_Kill = lumu_KillEffect?.Spawn(Player.local.creature.transform);
            lumu_Kill.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator CreatureHeadShotRoutine()
        {
            isPlaying = true;
            lumu_Headshot = lumu_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
            lumu_Headshot.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator PlayerKilledRoutine()
        {
            isPlaying = true;
            lumu_PlayerDeath = lumu_PlayerDeathEffect?.Spawn(Player.local.creature.transform);
            lumu_PlayerDeath.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator EnemySpawnWarningRoutine()
        {
            isPlaying = true;
            lumu_EnemyWarn = lumu_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            lumu_EnemyWarn.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
    }
}
