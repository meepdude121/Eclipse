using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eclipse.Bosses.FishronJr
{
    class SmallSharkMinion : ModNPC
    {
        private float maxSpeed = 12;

        public override void SetDefaults()
        {
            npc.life = npc.lifeMax = 200;
            npc.damage = 25;
            npc.noGravity = true;
            npc.aiStyle = -1;

            npc.width = 108;
            npc.height = 40;
        }

        public override bool PreAI()
        {
            npc.TargetClosest(true);

            if (npc.ai[0] >= npc.ai[1])
            {
                npc.ai[0] = 0;
                npc.ai[1] = 0;

                Vector2 targetVelocity = Main.player[npc.target].position - npc.Center;
                
                targetVelocity.Normalize();
                targetVelocity *= maxSpeed;

                targetVelocity = targetVelocity.RotatedByRandom(MathHelper.ToRadians(10));
                Projectile.NewProjectile(npc.Center, targetVelocity, ProjectileID.FlaironBubble, 30, 4, npc.whoAmI);

                npc.ai[1] = 20;// waits 40 frames. (1/3 a second)
                npc.ai[0] = 0;// sets counter to 0
            }

            float rot = npc.AngleFrom(Main.player[npc.target].position);
            
            if (rot < 180)
            {
                rot -= 180;
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }

            npc.rotation = rot;

            Utils.MoveTowardsPlayer(Main.npc[npc.whoAmI], Main.player[npc.target], 8, 3);

            npc.ai[0]++;
            return false;
        }
    }
}
