using System;
using System.Threading.Tasks;

namespace BotRetreat2017.Wpf.Framework.Services.Interfaces
{
    public interface IFileService
    {
        Task SaveTextFile(String fileName, String text);

        Task<String> LoadTextFile(String fileName);
    }
}