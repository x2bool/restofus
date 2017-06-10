using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Restofus.Components;
using Restofus.Navigation;
using Restofus.Utils;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace Restofus.Views
{
    public class NavigationPad : UserControl<NavigationPad.Context>
    {
        public NavigationPad()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public class Context : BaseContext
        {
            Navigator navigator;

            public Context(
                IResolver resolver,
                Navigator navigator) : base (resolver)
            {
                this.navigator = navigator;

                RootFiles = new ReactiveFileCollection();
                RootFiles.Add(BuildFileTree(Path.GetFullPath(".\\Files")));

                this.WhenAnyValue(x => x.SelectedFile)
                    .Where(f => f != null && !f.IsDirectory)
                    .Subscribe(ObserveFileSelection);

                Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    await navigator.Navigate(null);
                });
            }

            ReactiveFileCollection rootFiles;
            public ReactiveFileCollection RootFiles
            {
                get => rootFiles;
                set => this.RaiseAndSetIfChanged(ref rootFiles, value);
            }

            ReactiveFile selectedFile;
            public ReactiveFile SelectedFile
            {
                get => selectedFile;
                set => this.RaiseAndSetIfChanged(ref selectedFile, value);
            }

            void ObserveFileSelection(ReactiveFile file)
            {
                navigator.Navigate(file);
            }

            ReactiveFile BuildFileTree(string path)
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

                    var children = Enumerable.Concat(
                        Directory.EnumerateDirectories(path),
                        Directory.EnumerateFiles(path));

                    foreach (var child in children)
                    {
                        file.Files.Add(BuildFileTree(child));
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
        }
    }
}
