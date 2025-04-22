using SFML.System;

namespace SpaceShipGame3
{
    public static class PickableCollision
    {
        public static void SpaceshipPickableCollision(Entity first, Entity second)
        {
            if (first.IsPlayer && second.IsGasCan)
            {
                if (first as Player == null)
                    return;

                (first as Player).AddFuel((second as GasCan).AddPickableEffect());
                second.Position = new Vector2f(-50, -50);
            }
            else if (first.IsGasCan && second.IsPlayer)
            {
                if (second as Player == null)
                    return;

                (second as Player).AddFuel((first as GasCan).AddPickableEffect());
                first.Position = new Vector2f(-50, -50);
            }

            if (first.IsPlayer && second.IsPlasma)
            {
                if (first as Player == null)
                    return;

                (first as Player).AddPlasma((second as Plasma).AddPickableEffect());
                second.Position = new Vector2f(-50, -50);
            }
            else if (first.IsPlasma && second.IsPlayer)
            {
                if (second as Player == null)
                    return;

                (second as Player).AddPlasma((first as Plasma).AddPickableEffect());
                first.Position = new Vector2f(-50, -50);
            }

            if (first.IsPlayer && second.IsLife)
            {
                if (first as Player == null)
                    return;

                (first as Player).AddLife((second as Life).AddPickableEffect());
                second.Position = new Vector2f(-50, -50);
            }
            else if (first.IsLife && second.IsPlayer)
            {
                if (second as Player == null)
                    return;

                (second as Player).AddLife((first as Life).AddPickableEffect());
                first.Position = new Vector2f(-50, -50);
            }
        }
    }
}
