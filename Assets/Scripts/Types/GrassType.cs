public class GrassType : PokemonType
{
    public bool IsWeakTo(PokemonType type)
    {
        if (type is FireType)
        {
            return true;
        }

        return false;
    }
}