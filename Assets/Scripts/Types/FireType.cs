public class FireType : PokemonType {

    public bool IsWeakTo(PokemonType type)
    {
        if (type is WaterType) {
            return true;
        }

        return false;
    }
}