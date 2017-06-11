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

                this.WhenAnyValue(x => x.SelectedFile)
                    .Where(f => f != null && !f.IsDirectory)
                    .Subscribe(ObserveFileSelection);

                navigator.GetOpeningObservable()
                    .Subscribe(ObserveOpening);

                // TODO: remove this
                var root = navigator.BuildFileTree(Path.GetFullPath(".\\Files"));
                navigator.Open(root);
            }

            void ObserveOpening(ReactiveFile file)
            {
                RootFiles = new ReactiveFileCollection();
                RootFiles.Add(file);
            }

            void ObserveFileSelection(ReactiveFile file)
            {
                navigator.Select(file);
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
            
        }
    }
}
