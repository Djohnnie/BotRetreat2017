using System;

namespace BotRetreat2017.Wpf.Framework.Services.Interfaces
{
    public interface IFileExplorerService
    {
        String LoadFile(String filter);

        String SaveFile(String filter);
    }
}