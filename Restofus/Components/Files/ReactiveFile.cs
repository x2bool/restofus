using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restofus.Components.Files
{
    public class ReactiveFile : ReactiveObject
    {
        string path;
        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }

        string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        bool isDirectory;
        public bool IsDirectory
        {
            get => isDirectory;
            set => this.RaiseAndSetIfChanged(ref isDirectory, value);
        }

        ReactiveFileCollection files;
        public ReactiveFileCollection Files
        {
            get => files;
            set => this.RaiseAndSetIfChanged(ref files, value);
        }
    }
}
