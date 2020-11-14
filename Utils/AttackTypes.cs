using System;
using Terraria;

namespace Eclipse
{
    public static class AttackTypes
    {
        public enum FishronJrAttacks
        {
            Phase1WaterStream,
            Phase1Dash,
            Phase1SpawnSharkMinions,
            Phase1Heal
        }

        public enum EclipseNPC {
            FishronJr,
            SmallSharkMinion
        }

        public static int RandomAttackBasedOnNPC(EclipseNPC npc, int phase = 0)
        {
            switch (npc)
            {
                case EclipseNPC.FishronJr:
                    if (phase == 1) 
                    { 
                        return Main.rand.Next((int)FishronJrAttacks.Phase1WaterStream, (int)FishronJrAttacks.Phase1Heal);
                    } else  
                    {
                        return Main.rand.Next();
                    }
                case EclipseNPC.SmallSharkMinion:
                    return 0;
                default:
                    return -1;
            }
        }
    }
}
