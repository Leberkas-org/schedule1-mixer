namespace Schedule1.Mixer.Api.Mixing;

public record Ingredient(
    string Name,
    int Price,
    SimpleEffect SimpleEffect,
    AdvancedEffect[] AdvancedEffects);

public record Product(
    string Name,
    int Price,
    int Cost,
    ProductType Type,
    Effect[] Effects
)
{
    public Product AddIngredient(Ingredient ingredient)
    {
        var p = this with {Type = ProductType.Mixed};
        foreach (var effect in ingredient.AdvancedEffects)
        {
            var replace = Effects.FirstOrDefault(f => f.Name == effect.Replaces);
            if(replace is null) continue;

            p = p with
            {
                Cost = p.Cost + ingredient.Price,
                Effects = Effects.Except([replace]).Concat([effect]).ToArray()
            };
        }

        if (p.Effects.Any(f => f.Name == ingredient.SimpleEffect.Name)) return p;
        return p with { Effects = p.Effects.Concat([ingredient.SimpleEffect]).ToArray() };
    }
}

public record Effect(string Name);
public record SimpleEffect(string Name) : Effect(Name);
public record AdvancedEffect(string Name, string Replaces) : Effect(Name);

public enum ProductType
{
    Invalid = 0,
    Weed,
    Meth,
    Coca,
    Mixed
}