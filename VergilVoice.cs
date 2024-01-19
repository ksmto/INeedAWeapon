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
    public class VergilModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<VergilChip>();
        }
    }
    public class VergilChip : ThunderBehaviour
    {


        private Item item;
        EffectInstance vergil_SpawnGreet;
        EffectData vergil_SpawnGreetEffect;
        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();
            vergil_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWVergilGreet");



            if (item != null)
            {
                item.OnSnapEvent += VergilSnap;
                item.OnUnSnapEvent += VergilUnSnap;
            }

        }

        private void VergilUnSnap(Holder holder)
        {
            ModOptions.vergilActive = false;
        }

        private void VergilSnap(Holder holder)
        {
            VergilGreeting();
            ModOptions.butlrActive = false;
            ModOptions.cortanaActive = false;
            ModOptions.circActive = false;
            ModOptions.fretActive = false;
            ModOptions.lumuActive = false;
            ModOptions.misterchiefActive = false;
            ModOptions.matiActive = false;
            ModOptions.vergilActive = true;
        }
        public void VergilGreeting()
        {

                GameManager.local.StartCoroutine(VergilGreetRoutine());
        }
        public IEnumerator VergilGreetRoutine()
        {
            vergil_SpawnGreet = vergil_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            vergil_SpawnGreet.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            yield return null;
        }
    }

    public class VergilAnnouncer : HaloEvents
    {

        bool isPlaying;
        private EffectData vergil_PlayerDeathEffect, vergil_LowHPEffect, vergil_EnemyWarnEffect, vergil_KillEffect, vergil_KillHeadshotEffect, vergil_PlayerHealEffect;
        private EffectInstance vergil_PlayerDeath, vergil_LowHP, vergil_EnemyWarn, vergil_Kill, vergil_Headshot, vergil_PlayerHeal;
        public override void OnCatalogRefresh(EventTime eventTime)
        {
            vergil_PlayerHealEffect = Catalog.GetData<EffectData>("INAWVergilHeal");
            vergil_PlayerDeathEffect = Catalog.GetData<EffectData>("INAWVergilDeath");
            vergil_LowHPEffect = Catalog.GetData<EffectData>("INAWVergilLowHp");
            vergil_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWVergilEnemyWarn");
            vergil_KillEffect = Catalog.GetData<EffectData>("INAWVergilKill");
            vergil_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWVergilKillHeadshot");
        }


        /* EVENTS */

        public override void OnCreatureHeal(Creature creature, float heal, Creature healer, EventTime eventTime)
        {
            if (creature.isPlayer)
            {
                if (ModOptions.vergilActive && eventTime == EventTime.OnStart)
                {
                    if (AiRandom())
                    {
                        VergilPlayerHeal();
                    }
                }
            }

           
        }
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (ModOptions.vergilActive && eventTime == EventTime.OnStart)
            {
                if (creature.isPlayer)
                {
                    if (AiRandom())
                    {
                        {
                            VergilPlayerHurt();
                        }
                    }
                }
            }
        }
        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            {
                if (ModOptions.vergilActive && eventTime == EventTime.OnStart)
                {
                    {
                        if (AiRandom())
                        {
                            VergilKills(collisionInstance, creature);
                        }
                    }

                }

            }
        }
        public override void OnCreatureSpawn(Creature creature)
        {
            if (ModOptions.vergilActive && !creature.isPlayer)
            {
                if (AiRandom())
                {

                    VergilEnemyWarning();
                }

            }
        }

        /* METHODS */

        public void VergilPlayerHeal()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(PlayerHealRoutine());
            }
        }

        public void VergilPlayerHurt()
        {
            if (!isPlaying)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    GameManager.local.StartCoroutine(PlayerCriticalHurtRoutine());

                }
            }

        }
        public void VergilKills(CollisionInstance collisionInstance, Creature creature)
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

        public void VergilEnemyWarning()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(EnemySpawnWarningRoutine());
            }

        }

        public IEnumerator PlayerHealRoutine()
        {
            isPlaying = true;
            vergil_PlayerHeal = vergil_PlayerHealEffect?.Spawn(Player.local.creature.transform);
            vergil_PlayerHeal.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator PlayerCriticalHurtRoutine()
        {
            isPlaying = true;
            vergil_LowHP = vergil_LowHPEffect?.Spawn(Player.local.creature.transform);
            vergil_LowHP. Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator CreatureKillRoutine()
        {
            isPlaying = true;
            vergil_Kill = vergil_KillEffect?.Spawn(Player.local.creature.transform);
            vergil_Kill.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator CreatureHeadShotRoutine()
        {
            isPlaying = true;
            vergil_Headshot = vergil_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
            vergil_Headshot.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator PlayerKilledRoutine()
        {
            isPlaying = true;
            vergil_PlayerDeath = vergil_PlayerDeathEffect?.Spawn(Player.local.creature.transform);
            vergil_PlayerDeath.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator EnemySpawnWarningRoutine()
        {
            isPlaying = true;
            vergil_EnemyWarn = vergil_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            vergil_EnemyWarn.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
    }
}
