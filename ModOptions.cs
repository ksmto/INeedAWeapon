using ThunderRoad;

namespace INeedAWeapon
{
    internal class ModOptions
    {
        /* KILLSTREAK SETTINGS */
        [ModOptionButton]
        [ModOption(name = "Multikill Announcements", tooltip = "Toggles Multikill announcements.", order = 1, category = "Announcer", defaultValueIndex = 0, valueSourceName = nameof(EnabledOrDisabled))]
        public static bool MultikillAnnouncements;

        [ModOptionButton]
        [ModOption(name = "Killing Spree Announcements", tooltip = "Toggles killing spree announcements.", order = 2, category = "Announcer", defaultValueIndex = 0, valueSourceName = nameof(EnabledOrDisabled))]
        public static bool KillingSpreeAnnouncements;

        [ModOptionArrows]
        [ModOption(name = "Multikill Timer", tooltip = "How much time (seconds) between kills to get a multi kill.", order = 3, category = "Announcer", defaultValueIndex = 0, valueSourceName = nameof(MultikillTimeIntervals))]
        public static int MultikillTimeInterval;

        public static ModOptionBool[] EnabledOrDisabled = {
            new ModOptionBool("Enabled", true),
            new ModOptionBool("Disabled", false)
        };

        public static ModOptionInt[] MultikillTimeIntervals = {
            new ModOptionInt("4 Seconds", 4),
            new ModOptionInt("8 Seconds", 8),
            new ModOptionInt("16 Seconds", 16),
            new ModOptionInt("32 Seconds", 32)
        };

        /* AOE SCRIPT SETTINGS */
        [ModOptionCategory("Damage Settings", 1)]
        [ModOptionTooltip("Is the player susceptible to AoE effects")]
        [ModOptionOrder(1)]
        [ModOption("Player AoE Damage", defaultValueIndex = 0)]
        public static bool playerAOE;

        [ModOptionCategory("Damage Settings", 2)]
        [ModOptionTooltip("Changes the length of time AoE effects inflict damage")]
        [ModOption("AoE Duration", order = 3)]
        [ModOptionSave]
        [ModOptionSlider]
        public static float aoeDuration = 5f;

        [ModOptionCategory("Damage Settings", 2)]
        [ModOptionTooltip("Changes the total damage AoE weapons inflict")]
        [ModOption("AoE Damage", order = 4)]
        [ModOptionSave]
        [ModOptionSlider]
        public static float aoeDamage = 5f;

        /* CORTANA SETTINGS */
        public static ModOptionInt[] QuotePercentage =
        {
            new ModOptionInt("25%", 75),
            new ModOptionInt("50%", 50),
            new ModOptionInt("75%", 25),
            new ModOptionInt("100%", 0)
        };

        [ModOption(name: "AI Quote Chance", tooltip: "Chance of AI commenting on events", nameof(QuotePercentage))]
        [ModOptionCategory("AI Companion", 1)]
        [ModOptionArrows]
        [ModOptionOrder(2)]
        public static int quoteChance = 75;

        public static ModOptionInt[] optionquoteDelay =
         {
            new ModOptionInt("5", 5),
            new ModOptionInt("10", 10),
            new ModOptionInt("15", 15),
            new ModOptionInt("20", 20)
        };

        [ModOption(name: "AI Quote Delay", tooltip: "Delay between AI quotes playing", nameof(optionquoteDelay))]
        [ModOptionCategory("AI Companion", 1)]
        [ModOptionArrows]
        [ModOptionOrder(2)]
        public static int quoteDelay = 5;

        //AI companion enablers.  Need to be mod options, but don't need to be player visible
        public static bool butlrActive;
        public static bool cortanaActive;
        public static bool circActive;
        public static bool fretActive;
        public static bool lumuActive;
        public static bool misterchiefActive;
        public static bool matiActive;
        public static bool vergilActive;


        /* Energy Shield Settings */
        /*
         * Commenting these out until I have more time to work on the shields
        [ModOptionCategory("Energy Shield", 1)]
        [ModOptionTooltip("Is the energy shield enabled")]
        [ModOptionOrder(1)]
        [ModOption("Player AoE Damage", defaultValueIndex = 0)]
        public static bool energyShieldEnabled;

        public static ModOptionFloat[] optionEnergyHP =
 {
            new ModOptionFloat("25%", 25),
            new ModOptionFloat("50%", 50),
            new ModOptionFloat("75%", 75),
            new ModOptionFloat("100%", 100),
            new ModOptionFloat("200%", 200),
            new ModOptionFloat("300%", 300),
            new ModOptionFloat("400%", 400),
            new ModOptionFloat("500%", 500)
        };

        [ModOption(name: "Energy Shield Strength", tooltip: "How much health the energy shield has", nameof(optionEnergyHP))]
        [ModOptionCategory("Energy Shield", 2)]
        [ModOptionArrows]
        public static int energyShieldHP;


        public static ModOptionFloat[] optionShieldRecharge =
    {
            new ModOptionFloat("2", 2),
            new ModOptionFloat("4", 4),
            new ModOptionFloat("6", 6),
            new ModOptionFloat("8", 8)
        };

        [ModOption(name: "Energy Shield Recharge", tooltip: "How long it takes for your shield to start recharging after taking damage", nameof(optionShieldRecharge))]
        [ModOptionCategory("Energy Shield", 2)]
        [ModOptionArrows]
        public static int energyShieldDelay;
    }
        */

    }
}
