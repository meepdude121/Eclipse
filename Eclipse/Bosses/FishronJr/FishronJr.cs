using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

// this boss may be reworked into an eclipse event boss

namespace Eclipse.Bosses.FishronJr
{
    class FishronJr : ModNPC
    {
        //private bool secondPhase = false; // Don't need to assign variable because i want the first part to work 



        // overriding default values for statistics and the display name.
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishron Jr");
        }

        public override void SetDefaults()
        {
            npc.ai[2] = -1;
            npc.ai[0] = 0; // Counter (as the game runs at a static 60 frames / second, we can use a frame counter where every 60 frames is a second.)
            npc.ai[1] = 0; // time for counter to match to keep going

            npc.width = 194;
            npc.height = 144;

            npc.defense = 80;
            npc.life = npc.lifeMax = 22000;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            npc.boss = true;
            npc.value = Item.buyPrice(1, 0, 0, 0);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.life = npc.lifeMax = (int)(npc.lifeMax / Main.expertLife * bossLifeScale);
            npc.defense = 84;
        }

        public override bool PreAI()
        {
            switch (npc.ai[2])
            {
                case 0:
                    npc.TargetClosest(false);

                    if (npc.ai[0] / 5f % 1 != 0 && npc.ai[0] < npc.ai[1])
                    {
                        Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], Main.player[npc.target].Center, 4, ProjectileID.WaterGun, 30, 0f);
                    }

                    if (npc.ai[0] >= npc.ai[1])
                    {
                        npc.ai[2] = -1;
                        Main.NewText("Attack 0 (" + AttackTypes.FishronJrAttacks.Phase1WaterStream.ToString() + ") is finished.");
                        npc.ai[0] = 0;
                        npc.ai[1] = 30;
                    }

                    break;

                case 1:
                    if (npc.ai[0] < npc.ai[1])
                    {
                        int damage = 60;
                        float knockback = 5f;
                        int speed = 1;
                        npc.TargetClosest(false);

                        if (npc.ai[0] == 1)
                        {
                            for (int i = 0; i < Main.player.Length; i++)
                            {
                                Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], Main.player[i].position, ProjectileID.SharknadoBolt, speed, damage, knockback);
                                Main.NewText("Shot at: " + Main.player[i].position.ToString());
                            }
                        }

                        if (npc.ai[0] == 60||npc.ai[0] == 120||npc.ai[0] == 180)
                        {
                            // Utils.MoveTowardsTarget(Main.npc[npc.whoAmI], Main.player[npc.target].position, 15, 1); Use Utils.DashTowardsTarget() for smoother dashes!
                            Utils.DashTowardsTarget(Main.npc[npc.whoAmI], Main.player[npc.target].position, 40, 1, 1); // 40 is speed. Speed is blocks/2 per second

                            Main.PlaySound(SoundID.Roar); //play roar sound

                            //speed is blocks/second (I think) 
                            Main.NewText("Dashed Towards: " + Main.player[npc.target].position.ToString());
                        }
                        if (npc.ai[0] == 235)
                        {
                            npc.velocity = new Vector2(0, 0);
                        }
                    }

                    if (npc.ai[0] >= npc.ai[1])
                    {
                        npc.ai[2] = -1;
                        npc.ai[0] = 50;
                    }

                    break;
                case 2:
                    if (npc.ai[0] == 1)
                    {
                        for (int i = 0; i < Main.ActivePlayersCount * 2; i++)
                            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("SmallSharkMinion"));
                    }

                    if (npc.ai[0] >= npc.ai[1])
                    {
                        npc.ai[2] = -1;
                        npc.ai[0] = 15;
                    }
                    break;
                case 3:
                    if (npc.ai[0] == 1)
                    {
                        npc.HealEffect(Main.ActivePlayersCount * 400, true);
                        
                    }

                    if (npc.ai[0] >= npc.ai[1])
                    {
                        npc.ai[2] = -1;
                        npc.ai[0] = 0;
                    }
                    break;
            }


            if (npc.ai[1] <= npc.ai[0] && npc.ai[2] == -1)
            {
                Main.NewText("Counter is equal, picking new attack.");
                npc.ai[2] = AttackTypes.RandomAttackBasedOnNPC(AttackTypes.EclipseNPC.FishronJr, 1);

                switch (npc.ai[2])
                {
                    case 0:
                        npc.ai[1] = 360;
                        npc.ai[0] = 0;
                        break;
                    case 1:
                        npc.ai[1] = 360;
                        npc.ai[0] = 0;
                        break;
                    case 2:
                        npc.ai[1] = 30;
                        npc.ai[0] = 0;
                        break;
                    case 3:
                        npc.ai[1] = 30;
                        npc.ai[0] = 0;
                        break;
                }

                Main.NewText("Attack Type = " + npc.ai[2]);
            }
            npc.ai[0]++;
            return false; // prevent default AI from running.
        }
    }
}