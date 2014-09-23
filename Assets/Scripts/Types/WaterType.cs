public class WaterType  : PokemonType
{
    public bool IsWeakTo(PokemonType type)
    {
        if (type is GrassType || type is ElectricType)
        {
            return true;
        }

        return false;
    }
}