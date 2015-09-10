using Selkie.Windsor;

namespace MicroServices.DoctorsSlots.Nancy
{
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "MicroServices.";
        }
    }
}