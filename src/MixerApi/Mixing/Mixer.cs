using Akka.Actor;

namespace Schedule1.Mixer.Api.Mixing;

public record StartMixing(Product Product, Ingredient Ingredient);
public record MixingResult(Product Product, bool Modified);

public class Mixer : ReceiveActor
{
    public Mixer()
    {
        Receive<StartMixing>(msg =>
        {
            var product = msg.Product.AddIngredient(msg.Ingredient);
            var modified = product.Effects != msg.Product.Effects;

            Console.WriteLine(!modified ? "This was stupid" : string.Join(',', product.Effects.Select(f => f.Name)));

            Sender.Tell(new MixingResult(product, modified));
        });
    }
}