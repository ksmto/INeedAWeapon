using System.Collections;
using ThunderRoad;
using UnityEngine;

namespace INeedAWeapon {
    public class HaloAnnouncer : HaloEvents {
        int killCounter = 0;
        int killSpreeCounter = 0;

        // Multikill Effects
        private EffectData haloDoubleKillEffectData, haloTripleKillEffectData, haloOverKillEffectData, haloKilltacularEffectData, haloKilltrocityEffectData, haloKillimanjaroEffectData, haloKilltastropheEffectData, haloKillpocalypseEffectData, haloKillionaireEffectData;
        // Killing Spree Effects
        private EffectData haloKillingSpreeEffectData, haloKillingFrenzyEffectData, haloRunningRiotEffectData, haloRampageEffectData, haloUntouchableEffectData, haloInvincibleEffectData;

        public override void OnCatalogRefresh(EventTime eventTime) {
            base.OnCatalogRefresh(eventTime);

            // Multikill effects
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

        public override void OnMapChange(LevelData levelData, EventTime eventTime) {
            base.OnMapChange(levelData, eventTime);
            killSpreeCounter = 0;
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
            if (killCounter == 0) {
                GameManager.local.StartCoroutine(MultikillTimeInterval());
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");
            }

            if (killCounter == 1) {
                EffectInstance haloDoubleKillEffectInstance = haloDoubleKillEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloDoubleKillEffectInstance?.Play();
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");

            } else if (killCounter == 2) {
                EffectInstance haloTripleKillEffectInstance = haloTripleKillEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloTripleKillEffectInstance?.Play();
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");

            } else if (killCounter == 3) {
                EffectInstance haloOverKillEffectInstance = haloOverKillEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloOverKillEffectInstance?.Play();
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");

            } else if (killCounter == 4) {
                EffectInstance haloKilltacularEffectInstance = haloKilltacularEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloKilltacularEffectInstance?.Play();
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");

            } else if (killCounter == 5) {
                EffectInstance haloKilltrocityEffectInstance = haloKilltrocityEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloKilltrocityEffectInstance?.Play();
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");

            } else if (killCounter == 6) {
                EffectInstance haloKillimanjaroEffectInstance = haloKillimanjaroEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloKillimanjaroEffectInstance?.Play();
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");

            } else if (killCounter == 7) {
                EffectInstance haloKilltastropheEffectInstance = haloKilltastropheEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloKilltastropheEffectInstance?.Play();
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");

            } else if (killCounter == 8) {
                EffectInstance haloKillpocalypseEffectInstance = haloKillpocalypseEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloKillpocalypseEffectInstance?.Play();
                killCounter += 1;
                Debug.Log($"Multikill increased to {killCounter}");

            } else if (killCounter == 9) {
                EffectInstance haloKillionaireEffectInstance = haloKillionaireEffectData?.Spawn(creature.ragdoll.rootPart.transform);
                haloKillionaireEffectInstance?.Play();
                killCounter = 0;
                Debug.Log($"Multikill reset to {killCounter}");

            }
        }

        private void KillStreaks(Creature creature) {
            killSpreeCounter += 1;
            Debug.Log($"KillSpree increased to {killSpreeCounter}");

            if (!creature.isPlayer) {
                if (killSpreeCounter == 5) {
                    EffectInstance haloKillingSpreeEffectInstance = haloKillingSpreeEffectData?.Spawn(Player.local.creature.transform);
                    haloKillingSpreeEffectInstance?.Play();
                }

                if (killSpreeCounter == 10) {
                    EffectInstance haloKillingFrenzyEffectInstance = haloKillingFrenzyEffectData?.Spawn(Player.local.creature.transform);
                    haloKillingFrenzyEffectInstance?.Play();
                }

                if (killSpreeCounter == 15) {
                    EffectInstance haloKillingRunningRiotEffectInstance = haloRunningRiotEffectData?.Spawn(Player.local.creature.transform);
                    haloKillingRunningRiotEffectInstance?.Play();
                }

                if (killSpreeCounter == 20) {
                    EffectInstance haloKillingRampageEffectInstance = haloRampageEffectData?.Spawn(Player.local.creature.transform);
                    haloKillingRampageEffectInstance?.Play();
                }

                if (killSpreeCounter == 25) {
                    EffectInstance haloUntouchableEffectInstance = haloUntouchableEffectData?.Spawn(Player.local.creature.transform);
                    haloUntouchableEffectInstance?.Play();
                }

                if (killSpreeCounter == 30) {
                    EffectInstance haloInvincibleEffectInstance = haloInvincibleEffectData?.Spawn(Player.local.creature.transform);
                    haloInvincibleEffectInstance?.Play();
                    killSpreeCounter = 0;
                }
            } else {
                Debug.Log("Player died resetting killspree");
                killSpreeCounter = 0;
            }
        }

        public IEnumerator MultikillTimeInterval() {
            yield return new WaitForSeconds(ModOptions.MultikillTimeInterval);
            killCounter = 0;
            Debug.Log("MultiKill Timed out.");
            yield return null;
        }
    }
}