using Selkie.Windsor;

namespace MicroServices.Doctors.Nancy
{
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "MicroServices.";
        }
    }
}