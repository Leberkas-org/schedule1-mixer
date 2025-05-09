using Akka.Actor;

namespace Schedule1.Mixer.Api.Import;



public class ImportActor : ReceiveActor
{
    public ImportActor()
    {
        ReceiveAny(msg =>
        {
            
        });
    }
}