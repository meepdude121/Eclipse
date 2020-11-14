using Microsoft.Xna.Framework;
using Terraria;

namespace Eclipse
{
    public static class Utils
    {
        public static void MoveTowardsTarget(this NPC npc, Vector2 target, float speed, float turnResistance)
        {
            Vector2 move = target - npc.Center;
            float length = move.Length();
            if (length > speed)
            {
                move *= speed / length;
            }
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            length = move.Length();
            if (length > speed) move *= speed / length;
            npc.velocity = move;
        }

        public static void DashTowardsTarget(this NPC npc, Vector2 target, float speed, float turnResistance, float inertia = 5)
        {
            Vector2 move = target - npc.Center;
                //calculate velocity and "inertia".
                float length = move.Length();
                if (length > speed)
                {
                    move *= speed / length;
                }
                move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
                length = move.Length();
                if (length > speed) move *= speed / length;
                npc.velocity = (inertia * npc.velocity + move) / (inertia + 1);
        }

        public static void MoveTowardsPlayer(this NPC npc, Player player, float speed, float turnResistance, float inertia = 5)
        {
            Vector2 move = player.position - npc.Center;

                float length = move.Length();
                if (length > speed)
                {
                    move *= speed / length;
                }
                move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
                length = move.Length();
                if (length > speed) move *= speed / length;
            npc.velocity = (inertia * npc.velocity + move) / (inertia + 1);
        }

        public static void FireProjectileAtPosition(NPC npc, Vector2 targetPosition, float speed, int projectile, int damage, float knockback = 2f)
        {
            Vector2 targetVelocity = targetPosition - npc.Center;
            targetVelocity.Normalize();
            targetVelocity *= speed;
            Projectile.NewProjectile(npc.Center, targetVelocity, projectile, damage, knockback, npc.whoAmI);
        }
    }
}
