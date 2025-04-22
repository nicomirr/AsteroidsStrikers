using SFML.System;

namespace SpaceShipGame3
{
    public static class SpaceShipCollision
    {
        private static float damageTimerSpaceShipBullet = 0;
        private static float damageTimerSpaceShipPlayer = 0;
        private static float playerInvincibilityTime = 0.8f;

        public static void PlayerSmallSpaceShipCollision(Entity first, Entity second, float deltaTime)
        {
            if (first.IsEnemySpaceShip && second.IsPlayer)
            {
                if (second as Player == null)
                    return;

                if ((second as Player).Lives > 0)
                {
                    if (!(second as Player).Damaged)
                    {
                        damageTimerSpaceShipPlayer += deltaTime;

                        if (damageTimerSpaceShipPlayer == deltaTime)
                        {
                            (second as Player).Damaged = true;

                            (second as Player).ReceiveDamage();
                            damageTimerSpaceShipPlayer = 0;

                        }
                    }
                    else if ((second as Player).Damaged)
                    {
                        (second as Player).Invincibility = true;

                        if ((second as Player).InvincibilityTimer >= playerInvincibilityTime)
                        {
                            (second as Player).Damaged = false;
                        }
                    }

                }

            }
            else if (first.IsPlayer && second.IsEnemySpaceShip)
            {
                if (first as Player == null)
                    return;

                if ((first as Player).Lives > 0)
                {
                    if (!(first as Player).Damaged)
                    {
                        damageTimerSpaceShipPlayer += deltaTime;

                        if (damageTimerSpaceShipPlayer == deltaTime)
                        {
                            (first as Player).Damaged = true;

                            (first as Player).ReceiveDamage();
                            damageTimerSpaceShipPlayer = 0;

                        }
                    }
                    else if ((first as Player).Damaged)
                    {
                        (first as Player).Invincibility = true;

                        if ((first as Player).InvincibilityTimer >= playerInvincibilityTime)
                        {
                            (first as Player).Damaged = false;
                        }
                    }

                }

            }

        }

        public static void EnemySpaceShipBulletCollision(Entity first, Entity second, float deltaTime)
        {
            if (first.IsEnemySpaceShip && second.IsBullet)
            {
                second.Position = new Vector2f(-2000f, -2000f);
                (second as Bullet).IsActive = false;

                Vector2f position = first.Position;

                if (first.IsSmallSpaceShip)
                {
                    if ((first as SmallSpaceShip).Lives > 0)
                    {
                        damageTimerSpaceShipBullet += deltaTime;

                        if (damageTimerSpaceShipBullet == deltaTime)
                        {
                            (first as SmallSpaceShip).ReceiveDamage();
                            
                            damageTimerSpaceShipBullet = 0;
                        }
                    }
                }

                else if (first.IsMediumSpaceShip)
                {
                    if ((first as MediumSpaceShip).Lives > 0)
                    {
                        damageTimerSpaceShipBullet += deltaTime;

                        if (damageTimerSpaceShipBullet == deltaTime)
                        {
                            (first as MediumSpaceShip).ReceiveDamage();
                            (first as MediumSpaceShip).MediumSpaceShipDamaged.Play();

                            damageTimerSpaceShipBullet = 0;
                                                       
                        }
                    }

                }

                else if (first.IsBigSpaceShip)
                {
                    if ((first as BigSpaceShip).Lives > 0)
                    {
                        damageTimerSpaceShipBullet += deltaTime;

                        if (damageTimerSpaceShipBullet == deltaTime)
                        {
                            (first as BigSpaceShip).ReceiveDamage();
                            (first as BigSpaceShip).BigSpaceShipDamaged.Play();

                            damageTimerSpaceShipBullet = 0;

                        }
                    }

                }

            }

            else if (first.IsBullet && second.IsEnemySpaceShip)
            {
                first.Position = new Vector2f(-2000f, -2000f);
                (first as Bullet).IsActive = false;

                if (second.IsSmallSpaceShip)
                {
                    if ((second as SmallSpaceShip).Lives > 0)
                    {
                        damageTimerSpaceShipBullet += deltaTime;

                        if (damageTimerSpaceShipBullet == deltaTime)
                        {
                            (second as SmallSpaceShip).ReceiveDamage();
                            damageTimerSpaceShipBullet = 0;
                        }

                    }

                }

                else if (second.IsMediumSpaceShip)
                {
                    if ((second as MediumSpaceShip).Lives > 0)
                    {
                        damageTimerSpaceShipBullet += deltaTime;

                        if (damageTimerSpaceShipBullet == deltaTime)
                        {
                            (second as MediumSpaceShip).ReceiveDamage();
                            (second as MediumSpaceShip).MediumSpaceShipDamaged.Play();

                            damageTimerSpaceShipBullet = 0;
                        }

                    }

                }

                else if (second.IsBigSpaceShip)
                {
                    if ((second as BigSpaceShip).Lives > 0)
                    {
                        damageTimerSpaceShipBullet += deltaTime;

                        if (damageTimerSpaceShipBullet == deltaTime)
                        {
                            (second as BigSpaceShip).ReceiveDamage();
                            (second as BigSpaceShip).BigSpaceShipDamaged.Play();

                            damageTimerSpaceShipBullet = 0;
                                                     
                        }

                    }

                }

            }
        }
    }


}
