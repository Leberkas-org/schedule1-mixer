using Akka.Actor;
using Akka.Hosting;
using Microsoft.AspNetCore.Mvc;
using Schedule1.Mixer.Api.Mixing;

namespace Schedule1.Mixer.Api.Controller;

[Route("api/[controller]")]
public class GraphController(ActorRegistry registry)
{
    [HttpGet("create")]
    public async Task Create()
    {
        var ogKush = new Product("OG KUSH", 1, 0, ProductType.Weed, [
            new Effect("Calming")
        ]);

        var banana = new Ingredient("Banana", 2, new SimpleEffect("Gingeritis"),
            [
                new AdvancedEffect("Jennerising", "Paranoia"),
                new AdvancedEffect("Refreshing", "Long-Faced"),
                new AdvancedEffect("Seizure-Inducing", "Focused"),
                new AdvancedEffect("Smelly", "Toxic"),
                new AdvancedEffect("Sneaky", "Calming"),
                new AdvancedEffect("Thought-provoking", "Cyclopedian"),
                new AdvancedEffect("Anti-gravity", "Smelly"),
                new AdvancedEffect("Focused", "Disorienting"),
            ]);
            
        var mixer = registry.Get<Mixing.Mixer>();
        var result = await mixer.Ask<MixingResult>(new StartMixing(ogKush, banana));
        
        mixer.Tell(new StartMixing(result.Product, banana));
    }
}