using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Eclipse.Bosses.FishronJr
{
    class FishronJr : ModNPC
    {
        //private bool secondPhase = false; // Don't need to assign variable because i want the first part to work 
        private int attackType = -1;
        int counter = 0; // as the game runs at a static 60 frames / second, we can use a frame counter where every 60 frames is a second.
        int waitTime = 0;

        // overriding default values for statistics and the display name.
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishron Jr");
        }

        public override void SetDefaults()
        {
            npc.width = 194;
            npc.height = 144;

            npc.defense = 80;
            npc.life = npc.lifeMax = 22000;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.knockBackResist = 30f;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.lavaImmune = false;
            npc.boss = true;
            npc.Center = new Vector2();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.life = npc.lifeMax = (int)(npc.lifeMax / Main.expertLife * bossLifeScale);
            npc.defense = 84;
        }

        public override bool PreAI()
        {
            /*if (waitTime < counter)
            {
                counter = 0;
                npc.TargetClosest(false);
                npc.rotation = 90;
                if (!secondPhase)
                {
                    attackType = Main.rand.Next(0, 1);
                }
                else
                {
                    attackType = AttackTypes.RandomAttackBasedOnNPC(AttackTypes.EclipseNPC.FishronJr, 1);
                }
                switch (attackType)
                {
                    case 0:
                        {
                            npc.TargetClosest(false);
                            for (int i = 0; i < 10; i++)
                            {
                                Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], Main.player[npc.target].Center, 4, ProjectileID.WaterGun, 30, 0f);
                            }
                            waitTime = 360;
                            break;
                        }
                    case 1:
                        {
                            int damage = 10;
                            float knockback = 2f;
                            int speed = 2;
                            npc.TargetClosest(false);

                            Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], new Vector2(1, 0), ProjectileID.SharknadoBolt, speed, damage, knockback);
                            Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], new Vector2(0, 1), ProjectileID.SharknadoBolt, speed, damage, knockback);
                            Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], new Vector2(-1, 0), ProjectileID.SharknadoBolt, speed, damage, knockback);
                            Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], new Vector2(0, -1), ProjectileID.SharknadoBolt, speed, damage, knockback);
                            Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], new Vector2(.5f, .5f), ProjectileID.SharknadoBolt, speed, damage, knockback);
                            Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], new Vector2(-.5f, .5f), ProjectileID.SharknadoBolt, speed, damage, knockback);
                            Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], new Vector2(.5f, -.5f), ProjectileID.SharknadoBolt, speed, damage, knockback);
                            Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], new Vector2(-.5f, -.5f), ProjectileID.SharknadoBolt, speed, damage, knockback);

                            for (int i = 0; i < 60; i++)
                            {
                                npc.FaceTarget();
                            }
                            Utils.MoveTowardsPlayer(Main.npc[npc.whoAmI], Main.player[npc.target], 3, 1);

                            waitTime = 160;
                            break;
                        }

                    case 2:
                        {
                            for (int i = 0; i < Main.player.Length; i++)
                            {
                                NPC.SpawnNPC();
                            }

                            break;
                        }

                    case 3:
                        {
                            int healthToHeal = 200 * Main.player.Length;
                            if (npc.life + healthToHeal > npc.lifeMax)
                            {
                                npc.HealEffect(npc.lifeMax - npc.life);
                            }
                            else
                            {
                                npc.HealEffect(healthToHeal);
                            }

                            break;
                        }
                }
            }*/ // REDO ALL CODE

            switch (attackType)
            {
                case 0:
                    npc.TargetClosest(false);

                    if (counter / 5f % 1 != 0 && counter < waitTime)
                    {
                        Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], Main.player[npc.target].Center, 4, ProjectileID.WaterGun, 30, 0f);
                    }

                    if (counter >= waitTime)
                    {
                        attackType = -1;
                        Main.NewText("Attack 0 (" + AttackTypes.FishronJrAttacks.Phase1WaterStream.ToString() + ") is finished.");
                        counter = 0;
                        waitTime = 30;
                    }

                    break;

                case 1:
                    if (counter < waitTime)
                    {
                        int damage = 60;
                        float knockback = 5f;
                        int speed = 1;
                        npc.TargetClosest(false);

                        if (counter == 1)
                        {
                            for (int i = 0; i < Main.player.Length; i++)
                            {
                                Utils.FireProjectileAtPosition(Main.npc[npc.whoAmI], Main.player[i].position, ProjectileID.SharknadoBolt, speed, damage, knockback);
                                Main.NewText("Shot at: " + Main.player[i].position.ToString());
                            }
                        }

                        if (counter == 60||counter == 120||counter == 180)
                        {
                            // Utils.MoveTowardsTarget(Main.npc[npc.whoAmI], Main.player[npc.target].position, 15, 1); Use Utils.DashTowardsTarget() for smoother dashes!
                            Utils.DashTowardsTarget(Main.npc[npc.whoAmI], Main.player[npc.target].position, 40, 1, 1); // 40 is speed. Speed is blocks/2 per second

                            Main.PlaySound(SoundID.Roar); //play roar sound

                            //speed is blocks/second (I think) 
                            Main.NewText("Dashed Towards: " + Main.player[npc.target].position.ToString());
                        }
                        if (counter == 235)
                        {
                            npc.velocity = new Vector2(0, 0);
                        }
                    }

                    if (counter >= waitTime)
                    {
                        attackType = -1;
                        counter = 50;
                    }

                    break;
                case 2:
                    if (counter == 1)
                    {
                        for (int i = 0; i < Main.ActivePlayersCount * 2; i++)
                            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("SmallSharkMinion"));
                    }

                    if (counter >= waitTime)
                    {
                        attackType = -1;
                        counter = 15;
                    }
                    break;
                case 3:
                    if (counter == 1)
                    {
                        npc.HealEffect(Main.ActivePlayersCount * 400, true);
                        
                    }

                    if (counter >= waitTime)
                    {
                        attackType = -1;
                        counter = 0;
                    }
                    break;
            }


            if (waitTime <= counter && attackType == -1)
            {
                Main.NewText("Counter is equal, picking new attack.");
                attackType = AttackTypes.RandomAttackBasedOnNPC(AttackTypes.EclipseNPC.FishronJr, 1);

                switch (attackType)
                {
                    case 0:
                        waitTime = 360;
                        counter = 0;
                        break;
                    case 1:
                        waitTime = 360;
                        counter = 0;
                        break;
                    case 2:
                        waitTime = 30;
                        counter = 0;
                        break;
                    case 3:
                        waitTime = 30;
                        counter = 0;
                        break;
                }

                Main.NewText("Attack Type = " + attackType);
            }
            counter++;
            return false; // prevent default AI from running.
        }
    }
}