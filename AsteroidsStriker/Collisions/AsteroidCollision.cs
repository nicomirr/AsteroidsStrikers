using SFML.System;
using System;

namespace SpaceShipGame3
{    
    public static class AsteroidCollision
    {
        private static float damageTimerAsteroidBullet = 0;
        private static float damageTimerAsteroidPlayer = 0;        
        private static float playerInvincibilityTime = 0.8f;

        public static void PlayerAsteroidCollision(Entity first, Entity second, float deltaTime)
        {
            if (first.IsAsteroid && second.IsPlayer)
            {              
                if ((second as Player) == null)
                    return;

                else if((second as Player).Lives > 0)
                {
                    if (!(second as Player).Damaged)
                    {
                        damageTimerAsteroidPlayer += deltaTime;

                        if (damageTimerAsteroidPlayer == deltaTime)
                        {
                            (second as Player).Damaged = true;

                            (second as Player).ReceiveDamage();
                            damageTimerAsteroidPlayer = 0;
                                                      
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
            else if (first.IsPlayer && second.IsAsteroid)
            {              
                if (first as Player == null)
                    return;

                else if ((first as Player).Lives > 0)
                {
                    if(!(first as Player).Damaged)
                    {
                        damageTimerAsteroidPlayer += deltaTime;

                        if (damageTimerAsteroidPlayer == deltaTime)
                        {
                            (first as Player).Damaged = true;

                            (first as Player).ReceiveDamage();
                            damageTimerAsteroidPlayer = 0;
                                                       
                        }
                    }

                    else if((first as Player).Damaged)
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

        public static void AsteroidBulletCollision(Entity first, Entity second, float deltaTime)
        {
            if (first.IsAsteroid && second.IsBullet)
            {
                second.Position = new Vector2f(-2000f, -2000f);   
                (second as Bullet).IsActive = false;

                Vector2f position = first.Position;

                if (first.IsSmallAsteroid)
                {
                    if ((first as SmallAsteroid).Lives > 0)
                    {
                        damageTimerAsteroidBullet += deltaTime;

                        if (damageTimerAsteroidBullet == deltaTime)
                        {
                            (first as SmallAsteroid).ReceiveDamage();
                            (first as SmallAsteroid).AsteroidDamaged.Play();

                            damageTimerAsteroidBullet = 0;

                            if ((first as SmallAsteroid).Lives == 0)
                                (first as SmallAsteroid).AsteroidDestroyed.Play();
                        }
                    }
                }

                else if (first.IsMediumAsteroid)
                {
                    if ((first as MediumAsteroid).Lives > 0)
                    {
                        damageTimerAsteroidBullet += deltaTime;

                        if (damageTimerAsteroidBullet == deltaTime)
                        {
                            (first as MediumAsteroid).ReceiveDamage();
                            (first as MediumAsteroid).AsteroidDamaged.Play();
                            damageTimerAsteroidBullet = 0;

                            if ((first as MediumAsteroid).Lives == 0)
                                (first as MediumAsteroid).AsteroidDestroyed.Play();
                        }
                    }

                }

                else if (first.IsBigAsteroid)
                {
                    if ((first as BigAsteroid).Lives > 0)
                    {
                        damageTimerAsteroidBullet += deltaTime;

                        if (damageTimerAsteroidBullet == deltaTime)
                        {
                            (first as BigAsteroid).ReceiveDamage();
                            (first as BigAsteroid).AsteroidDamaged.Play();
                            damageTimerAsteroidBullet = 0;

                            if ((first as BigAsteroid).Lives == 0)
                                (first as BigAsteroid).AsteroidDestroyed.Play();
                        }
                    }

                }

            }
            else if (first.IsBullet && second.IsAsteroid)
            {
                first.Position = new Vector2f(-2000f, -2000f);
                (first as Bullet).IsActive = false;

                if (second.IsSmallAsteroid)
                {
                    if ((second as SmallAsteroid).Lives > 0)
                    {
                        damageTimerAsteroidBullet += deltaTime;

                        if (damageTimerAsteroidBullet == deltaTime)
                        {
                            (second as SmallAsteroid).ReceiveDamage();
                            (second as SmallAsteroid).AsteroidDamaged.Play();
                            damageTimerAsteroidBullet = 0;

                            if ((second as SmallAsteroid).Lives == 0)
                                (second as SmallAsteroid).AsteroidDestroyed.Play();
                        }

                    }

                }

                else if (second.IsMediumAsteroid)
                {
                    if ((second as MediumAsteroid).Lives > 0)
                    {
                        damageTimerAsteroidBullet += deltaTime;

                        if(damageTimerAsteroidBullet == deltaTime)
                        {                            
                            (second as MediumAsteroid).ReceiveDamage();                                                      
                            (second as MediumAsteroid).AsteroidDamaged.Play();                                                      
                            damageTimerAsteroidBullet = 0;

                            if ((second as MediumAsteroid).Lives == 0)
                                (second as MediumAsteroid).AsteroidDestroyed.Play();
                        }

                    }
                    
                }

                else if (second.IsBigAsteroid)
                {
                    if ((second as BigAsteroid).Lives > 0)
                    {
                        damageTimerAsteroidBullet += deltaTime;

                        if (damageTimerAsteroidBullet == deltaTime)
                        {                            
                            (second as BigAsteroid).ReceiveDamage();                            
                            (second as BigAsteroid).AsteroidDamaged.Play();                            
                            damageTimerAsteroidBullet = 0;

                            if ((second as BigAsteroid).Lives == 0)
                                (second as BigAsteroid).AsteroidDestroyed.Play();
                        }

                    }
                   
                }

                
            }
        }

    }
}
