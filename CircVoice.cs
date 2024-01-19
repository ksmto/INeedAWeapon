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
    public class CircModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<CircChip>();
        }
    }
    public class CircChip : ThunderBehaviour
    {


        private Item item;
        EffectInstance circ_SpawnGreet;
        EffectData circ_SpawnGreetEffect;
        protected override void ManagedOnEnable()
        {
            base.ManagedOnEnable();
            item = GetComponent<Item>();
            circ_SpawnGreetEffect = Catalog.GetData<EffectData>("INAWCircGreet");



            if (item != null)
            {
                item.OnSnapEvent += CircSnap;
                item.OnUnSnapEvent += CircUnSnap;
            }

        }

        private void CircUnSnap(Holder holder)
        {
            ModOptions.circActive = false;
        }

        private void CircSnap(Holder holder)
        {
            CircGreeting();
            ModOptions.butlrActive = false;
            ModOptions.cortanaActive = false;
            ModOptions.circActive = true;
            ModOptions.fretActive = false;
            ModOptions.lumuActive = false;
            ModOptions.misterchiefActive = false;
            ModOptions.matiActive = false;
            ModOptions.vergilActive = false;
        }
        public void CircGreeting()
        {

                GameManager.local.StartCoroutine(CircGreetRoutine());
        }
        public IEnumerator CircGreetRoutine()
        {
            circ_SpawnGreet = circ_SpawnGreetEffect?.Spawn(Player.local.creature.transform);
            circ_SpawnGreet.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            yield return null;
        }
    }

    public class CircAnnouncer : HaloEvents
    {

        bool isPlaying;
        private EffectData circ_PlayerDeathEffect, circ_LowHPEffect, circ_EnemyWarnEffect, circ_KillEffect, circ_KillHeadshotEffect, circ_PlayerHealEffect;
        private EffectInstance circ_PlayerDeath, circ_LowHP, circ_EnemyWarn, circ_Kill, circ_Headshot, circ_PlayerHeal;
        public override void OnCatalogRefresh(EventTime eventTime)
        {
            circ_PlayerHealEffect = Catalog.GetData<EffectData>("INAWCircHeal");
            circ_PlayerDeathEffect = Catalog.GetData<EffectData>("INAWCircDeath");
            circ_LowHPEffect = Catalog.GetData<EffectData>("INAWCircLowHp");
            circ_EnemyWarnEffect = Catalog.GetData<EffectData>("INAWCircEnemyWarn");
            circ_KillEffect = Catalog.GetData<EffectData>("INAWCircKill");
            circ_KillHeadshotEffect = Catalog.GetData<EffectData>("INAWCircKillHeadshot");
        }


        /* EVENTS */

        public override void OnCreatureHeal(Creature creature, float heal, Creature healer, EventTime eventTime)
        {
            if (creature.isPlayer)
            {
                if (ModOptions.circActive && eventTime == EventTime.OnStart)
                {
                    if (AiRandom())
                    {
                        CircPlayerHeal();
                    }
                }
            }

           
        }
        public override void OnCreatureHit(Creature creature, CollisionInstance collisionInstance, EventTime eventTime)
        {
            if (ModOptions.circActive && eventTime == EventTime.OnStart)
            {
                if (creature.isPlayer)
                {
                    if (AiRandom())
                    {
                        {
                            CircPlayerHurt();
                        }
                    }
                }
            }
        }
        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime)
        {
            {
                if (ModOptions.circActive && eventTime == EventTime.OnStart)
                {
                    {
                        if (AiRandom())
                        {
                            CircKills(collisionInstance, creature);
                        }
                    }

                }

            }
        }
        public override void OnCreatureSpawn(Creature creature)
        {
            if (ModOptions.circActive && !creature.isPlayer)
            {
                if (AiRandom())
                {

                    CircEnemyWarning();
                }

            }
        }

        /* METHODS */

        public void CircPlayerHeal()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(PlayerHealRoutine());
            }
        }

        public void CircPlayerHurt()
        {
            if (!isPlaying)
            {
                if (Player.currentCreature.currentHealth / Player.currentCreature.maxHealth * 100 <= 25f)
                {
                    GameManager.local.StartCoroutine(PlayerCriticalHurtRoutine());

                }
            }

        }
        public void CircKills(CollisionInstance collisionInstance, Creature creature)
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

        public void CircEnemyWarning()
        {
            if (!isPlaying)
            {
                GameManager.local.StartCoroutine(EnemySpawnWarningRoutine());
            }

        }

        public IEnumerator PlayerHealRoutine()
        {
            isPlaying = true;
            circ_PlayerHeal = circ_PlayerHealEffect?.Spawn(Player.local.creature.transform);
            circ_PlayerHeal.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator PlayerCriticalHurtRoutine()
        {
            isPlaying = true;
            circ_LowHP = circ_LowHPEffect?.Spawn(Player.local.creature.transform);
            circ_LowHP. Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator CreatureKillRoutine()
        {
            isPlaying = true;
            circ_Kill = circ_KillEffect?.Spawn(Player.local.creature.transform);
            circ_Kill.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator CreatureHeadShotRoutine()
        {
            isPlaying = true;
            circ_Headshot = circ_KillHeadshotEffect?.Spawn(Player.local.creature.transform);
            circ_Headshot.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
        public IEnumerator PlayerKilledRoutine()
        {
            isPlaying = true;
            circ_PlayerDeath = circ_PlayerDeathEffect?.Spawn(Player.local.creature.transform);
            circ_PlayerDeath.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }

        public IEnumerator EnemySpawnWarningRoutine()
        {
            isPlaying = true;
            circ_EnemyWarn = circ_EnemyWarnEffect?.Spawn(Player.local.creature.transform);
            circ_EnemyWarn.Play();
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            isPlaying = false;
            yield return null;
        }
    }
}
