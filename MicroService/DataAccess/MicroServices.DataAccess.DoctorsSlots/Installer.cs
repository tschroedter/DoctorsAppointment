using Selkie.Windsor;

namespace MicroServices.DataAccess.DoctorsSlots
{
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "MicroServices.";
        }
    }
}