using SFML.System;


namespace SpaceShipGame3
{
    public static class ShootingStarCollision
    {
        public static void ShootingStarBulletCollision(Entity first, Entity second)
        {
            if (first.IsBullet && second.IsShootingStar)
            {
                first.Position = new Vector2f(-100, -100);

                second.Position = new Vector2f(-50, -50);

                (second as ShootingStar).Destroyed = true;


            }

            else if (second.IsBullet && first.IsShootingStar)
            {
                second.Position = new Vector2f(-100, -100);

                first.Position = new Vector2f(-50, -50);
                (first as ShootingStar).Destroyed = true;
            }
        }

    }
}
