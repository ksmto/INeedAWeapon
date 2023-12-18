using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ThunderRoad;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace INeedAWeapon {
    public class HaloAnnouncer : HaloEvents {
        public bool isPlaying;
        int killCounter = 0;
        int killSpreeCounter = 0;
        // Multikill Effects
        private EffectData haloDoubleKillEffectData, haloTripleKillEffectData, haloOverKillEffectData, haloKilltacularEffectData, haloKilltrocityEffectData, haloKillimanjaroEffectData, haloKilltastropheEffectData, haloKillpocalypseEffectData, haloKillionaireEffectData;
        // Killing Spree Effects
        private EffectData haloKillingSpreeEffectData, haloKillingFrenzyEffectData, haloRunningRiotEffectData, haloRampageEffectData, haloUntouchableEffectData, haloInvincibleEffectData;
 
        public override void OnCatalogRefresh(EventTime eventTime) {
            base.OnCatalogRefresh(eventTime);
            EffectData();
        }

        public override void OnMapChange(LevelData levelData, EventTime eventTime) {
            base.OnMapChange(levelData, eventTime);
            killSpreeCounter = 0;
            killCounter = 0;
        }

        public override void OnCreatureKill(Creature creature, Player player, CollisionInstance collisionInstance, EventTime eventTime) {
            base.OnCreatureKill(creature, player, collisionInstance, eventTime);
            if (ModOptions.KillingSpreeAnnouncements) {
                KillStreaks(creature);
            }

            if (ModOptions.MultikillAnnouncements) {
                Multikills(creature);
            }
        }

        private void Multikills(Creature creature) {

            killCounter += 1;
            Debug.Log("kill counter is at " + killCounter);
            if (killCounter == 1) {
                GameManager.local.StartCoroutine(MultikillTimeInterval());
            }

            else if (killCounter == 3) {
                Debug.Log("playing Double Kill.  Kill Counter = " + killCounter);
                GameManager.local.StartCoroutine(DoubleKillRoutine());


            }
            else if (killCounter == 4) {
                GameManager.local.StartCoroutine(TripleKillRoutine());
                Debug.Log("playing triple Kill.  Kill Counter = " + killCounter);


            }
            else if (killCounter == 5) {
                Debug.Log("playing overkill .  Kill Counter = " + killCounter);
                GameManager.local.StartCoroutine(OverKillRoutine());



            }
            else if (killCounter == 6) {
                Debug.Log("playing Killtacular.  Kill Counter = " + killCounter);
                GameManager.local.StartCoroutine(KilltacularRoutine());


            }
            else if (killCounter == 7) {
                Debug.Log("playing Killtrocity.  Kill Counter = " + killCounter);
                GameManager.local.StartCoroutine(KilltrocityRoutine());


            }
            else if (killCounter == 8) {
                Debug.Log("playing Killimanjaro.  Kill Counter = " + killCounter);
                GameManager.local.StartCoroutine(KillimanjaroRoutine());


            }
            else if (killCounter == 9) {
                Debug.Log("playing Killtastrophe.  Kill Counter = " + killCounter);
                GameManager.local.StartCoroutine(KilltastropheRoutine());


            }
            else if (killCounter == 10) {
                Debug.Log("playing killpocalypse.  Kill Counter = " + killCounter);
                GameManager.local.StartCoroutine(KillpocalypseRoutine());

            }
            else if (killCounter == 11) {
                Debug.Log("playing killionaire.  Kill Counter = " + killCounter);
                GameManager.local.StartCoroutine(KillionaireRoutine());
                killCounter = 0;

            }
        }

        private void KillStreaks(Creature creature) {
            killSpreeCounter += 1;
            Debug.Log ("Killing spree counter = " + killSpreeCounter);
            if (!creature.isPlayer) {
                if (killSpreeCounter == 6) {
                    GameManager.local.StartCoroutine(KillingSpreeRoutine());
                }

                if (killSpreeCounter == 11) {
                    GameManager.local.StartCoroutine(KillingFrenzyRoutine());

                }

                if (killSpreeCounter == 16) {
                    GameManager.local.StartCoroutine(RunningRiotRoutine());

                }

                if (killSpreeCounter == 21) {
                    GameManager.local.StartCoroutine(RampageRoutine());

                }

                if (killSpreeCounter == 26) {
                    GameManager.local.StartCoroutine(UntouchableRoutine());

                }

                if (killSpreeCounter == 31) {
                    GameManager.local.StartCoroutine(InvincibleRoutine());
                    killSpreeCounter = 0;
                }
            } else if (creature.isPlayer) {
                killSpreeCounter = 0;
            }
        }









        /*  KillingSpree Routines */

        public IEnumerator KillingSpreeRoutine()
        {
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            EffectInstance haloKillingSpreeEffectInstance = haloKillingSpreeEffectData?.Spawn(Player.local.creature.transform);
            haloKillingSpreeEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator KillingFrenzyRoutine()
        {
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            EffectInstance haloKillingFrenzyEffectInstance = haloKillingFrenzyEffectData?.Spawn(Player.local.creature.transform);
            haloKillingFrenzyEffectInstance?.Play();
            isPlaying = false;
            yield return null;
        }
        public IEnumerator RunningRiotRoutine()
        {
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            EffectInstance haloKillingRunningRiotEffectInstance = haloRunningRiotEffectData?.Spawn(Player.local.creature.transform);
            haloKillingRunningRiotEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator RampageRoutine()
        {
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            EffectInstance haloKillingRampageEffectInstance = haloRampageEffectData?.Spawn(Player.local.creature.transform);
            haloKillingRampageEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator UntouchableRoutine()
        {
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            EffectInstance haloUntouchableEffectInstance = haloUntouchableEffectData?.Spawn(Player.local.creature.transform);
            haloUntouchableEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator InvincibleRoutine()
        {
            yield return new WaitForSeconds(ModOptions.quoteDelay);
            EffectInstance haloInvincibleEffectInstance = haloInvincibleEffectData?.Spawn(Player.local.creature.transform);
            haloInvincibleEffectInstance?.Play();
            yield return null;
        }
        /*( MULTIKILL ROUTINES */
        public IEnumerator DoubleKillRoutine()
        {
            EffectInstance haloDoubleKillEffectInstance = haloDoubleKillEffectData?.Spawn(Player.local.creature.transform);
            haloDoubleKillEffectInstance?.Play();
            yield return null;          
        }
        public IEnumerator TripleKillRoutine()
        {
            EffectInstance haloTripleKillEffectInstance = haloTripleKillEffectData?.Spawn(Player.local.creature.transform);
            haloTripleKillEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator OverKillRoutine()
        {
            EffectInstance haloOverKillEffectInstance = haloOverKillEffectData?.Spawn(Player.local.creature.transform);
            haloOverKillEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator KilltacularRoutine()
        {
            EffectInstance haloKilltacularEffectInstance = haloKilltacularEffectData?.Spawn(Player.local.creature.transform);
            haloKilltacularEffectInstance?.Play();
            yield return null;
        }

        public IEnumerator KilltrocityRoutine()
        {
            EffectInstance haloKilltrocityEffectInstance = haloKilltrocityEffectData?.Spawn(Player.local.creature.transform);
            haloKilltrocityEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator KillimanjaroRoutine()
        {
            EffectInstance haloKillimanjaroEffectInstance = haloKillimanjaroEffectData?.Spawn(Player.local.creature.transform);
            haloKillimanjaroEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator KilltastropheRoutine()
        {
            EffectInstance haloKilltastropheEffectInstance = haloKilltastropheEffectData?.Spawn(Player.local.creature.transform);
            haloKilltastropheEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator KillpocalypseRoutine()
        {
            EffectInstance haloKillpocalypseEffectInstance = haloKillpocalypseEffectData?.Spawn(Player.local.creature.transform);
            haloKillpocalypseEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator KillionaireRoutine()
        {

            EffectInstance haloKillionaireEffectInstance = haloKillionaireEffectData?.Spawn(Player.local.creature.transform);
            haloKillionaireEffectInstance?.Play();
            yield return null;
        }
        public IEnumerator MultikillTimeInterval() {

            yield return new WaitForSeconds(ModOptions.MultikillTimeInterval);
            killCounter = 0;
            Debug.Log("MultiKill Timed out.");
            yield return null;
        }
        void EffectData()
        {
            haloDoubleKillEffectData = Catalog.GetData<EffectData>("HaloDoubleKillQuote");
            haloTripleKillEffectData = Catalog.GetData<EffectData>("HaloTripleKillQuote");
            haloOverKillEffectData = Catalog.GetData<EffectData>("HaloOverKillQuote");
            haloKilltacularEffectData = Catalog.GetData<EffectData>("HaloKilltacularQuote");
            haloKilltrocityEffectData = Catalog.GetData<EffectData>("HaloKilltrocityQuote");
            haloKillimanjaroEffectData = Catalog.GetData<EffectData>("HaloKillimanjaroQuote");
            haloKilltastropheEffectData = Catalog.GetData<EffectData>("HaloKilltastropheQuote");
            haloKillpocalypseEffectData = Catalog.GetData<EffectData>("HaloKillpocalypseQuote");
            haloKillionaireEffectData = Catalog.GetData<EffectData>("HaloKillionaireQuote");

            // Killing spree effects 
            haloKillingSpreeEffectData = Catalog.GetData<EffectData>("HaloKillingSpreeQuote");
            haloKillingFrenzyEffectData = Catalog.GetData<EffectData>("HaloKillingFrenzyQuote");
            haloRunningRiotEffectData = Catalog.GetData<EffectData>("HaloRunningRiotQuote");
            haloRampageEffectData = Catalog.GetData<EffectData>("HaloRampageQuote");
            haloUntouchableEffectData = Catalog.GetData<EffectData>("HaloUntouchableQuote");
            haloInvincibleEffectData = Catalog.GetData<EffectData>("HaloInvincibleQuote");
            
        }
    }
}
