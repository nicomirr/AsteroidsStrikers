using System;
using System.Collections.Generic;


namespace SpaceShipGame3
{
    public static class CollisionsHandler
    {
        private static readonly List<Entity> entities = new List<Entity>();

        private static void SolveCollision(Entity first, Entity second, float deltaTime)
        {
            AsteroidCollision.PlayerAsteroidCollision(first, second, deltaTime);
            AsteroidCollision.AsteroidBulletCollision(first, second, deltaTime);
            SpaceShipCollision.PlayerSmallSpaceShipCollision(first, second, deltaTime);
            SpaceShipCollision.EnemySpaceShipBulletCollision(first, second, deltaTime);
            ShootingStarCollision.ShootingStarBulletCollision(first, second);
            PickableCollision.SpaceshipPickableCollision(first, second);                       
        }                

        public static void AddEntity(Entity entity)
        {
            if (entities.Contains(entity))
            {
                Console.WriteLine("Error. The entity is already registered inside the Collisions Handler!");
                return;
            }

            entities.Add(entity);
        }

        public static void RemoveEntity(Entity entity)
        {
            if (!entities.Contains(entity))
            {
                Console.WriteLine("Error. The entity is not registered inside the Collisions Handler!");
                return;
            }

            entities.Remove(entity);
        }

        public static void Update(float deltaTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                for (int j = i + 1; j < entities.Count; j++)     
                {
                    if (entities[i].IsColliding(entities[j]))
                        SolveCollision(entities[i], entities[j], deltaTime);
                }
            }
        }
    }
}
