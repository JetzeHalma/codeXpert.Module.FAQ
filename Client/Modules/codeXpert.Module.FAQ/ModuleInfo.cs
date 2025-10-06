using Oqtane.Models;
using Oqtane.Modules;

namespace codeXpert.Module.FAQ
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "FAQ",
            Description = "Frequently asked questions module",
            Version = "1.0.0",
            ServerManagerType = "codeXpert.Module.FAQ.Manager.FAQManager, codeXpert.Module.FAQ.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "codeXpert.Module.FAQ.Shared.Oqtane",
            PackageName = "codeXpert.Module.FAQ" 
        };
    }
}
