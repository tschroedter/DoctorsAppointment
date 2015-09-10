using Selkie.Windsor;

namespace MicroServices.Slots.Nancy
{
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "MicroServices.";
        }
    }
}