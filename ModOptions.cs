using ThunderRoad;

namespace INeedAWeapon {
    internal class ModOptions {
        [ModOptionButton]
        [ModOption(name = "Multikill Announcements", tooltip = "Whether or not if multikilling is enabled.", order = 1, category = "Announcer", defaultValueIndex = 0, valueSourceName = nameof(EnabledOrDisabled))]
        public static bool MultikillAnnouncements;

        [ModOptionButton]
        [ModOption(name = "Killing Spree Announcements", tooltip = "Whether or not if killing sprees are enabled.", order = 2, category = "Announcer", defaultValueIndex = 0, valueSourceName = nameof(EnabledOrDisabled))]
        public static bool KillingSpreeAnnouncements;

        [ModOptionArrows]
        [ModOption(name = "Multikill Timer", tooltip = "How long of time (seconds) between kills to get a multi kill.", order = 3, category = "Announcer", defaultValueIndex = 0, valueSourceName = nameof(MultikillTimeIntervals))]
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
    }
}