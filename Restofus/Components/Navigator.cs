using Restofus.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restofus.Components
{
    public class Navigator
    {
        public event EventHandler<ReactiveFile> Selected;

        public event EventHandler<ReactiveFile> Opened;

        public Task Select(ReactiveFile file)
        {
            Selected?.Invoke(this, file);

            return Task.CompletedTask;
        }

        public Task Open(ReactiveFile file)
        {
            Opened?.Invoke(this, file);

            return Task.CompletedTask;
        }

        public ReactiveFile BuildFileTree(string path)
        {
            ReactiveFile file = null;

            if (Directory.Exists(path))
            {
                file = new ReactiveFile
                {
                    Path = new DirectoryInfo(path).FullName,
                    Name = new DirectoryInfo(path).Name,
                    IsDirectory = true,
                    Files = new ReactiveFileCollection()
                };

                var dirs = Directory.EnumerateDirectories(path);

                foreach (var child in dirs)
                {
                    file.Files.Add(BuildFileTree(child));
                }

                var groups = Directory.EnumerateFiles(path)
                    .Where(f => f.EndsWith(".env") || f.EndsWith(".req"))
                    .GroupBy(GetGroupingFilename)
                    .OrderBy(g => g.Key);

                foreach (var group in groups)
                {
                    var files = group.OrderBy(f => f.Length).ToArray();
                    
                    var file1 = BuildFileTree(files[0]);

                    if (files.Length > 1)
                    {
                        var file2 = BuildFileTree(files[1]);

                        file1.Files = new ReactiveFileCollection();
                        file1.Files.Add(file2);
                    }

                    file.Files.Add(file1);
                }
            }
            else if (File.Exists(path))
            {
                file = new ReactiveFile
                {
                    Path = new FileInfo(path).FullName,
                    Name = new FileInfo(path).Name
                };
            }

            return file;
        }

        string GetGroupingFilename(string path)
        {
            if (path.EndsWith(".user.env"))
            {
                return path.Substring(0, path.Length - 9) + ".env";
            }

            if (path.EndsWith(".meta.req"))
            {
                return path.Substring(0, path.Length - 9) + ".req";
            }

            return path;
        }
    }

    public static class NavigatorExtensions
    {
        public static IObservable<ReactiveFile> GetSelectionObservable(this Navigator navigator)
        {
            return Observable.FromEventPattern<ReactiveFile>(
                        h => navigator.Selected += h, h => navigator.Selected -= h)
                    .Select(e => e.EventArgs);
        }

        public static IObservable<ReactiveFile> GetOpeningObservable(this Navigator navigator)
        {
            return Observable.FromEventPattern<ReactiveFile>(
                        h => navigator.Opened += h, h => navigator.Opened -= h)
                    .Select(e => e.EventArgs);
        }
    }
}
