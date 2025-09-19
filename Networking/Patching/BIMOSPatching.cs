using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace KadenZombie8.BIMOS
{
    static class BIMOSPatching
    {
        public const string HarmonyID = "com.bimos";
        public static Harmony Harmony;

        static BIMOSPatching()
        {
            if (Harmony != null) return;
            Harmony = new Harmony(HarmonyID);
            Debug.Log($"Harmony Patching...");
            try
            {
                Harmony.PatchAll(Assembly.GetExecutingAssembly());
                Debug.Log($"Harmony Patched!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Harmony Patch Failed! Exception:({e})");
            }
        }
    }
}
