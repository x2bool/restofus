using ReactiveUI;
using Restofus.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Restofus.Components
{
    public class BaseContext : ReactiveObject, IDisposable
    {
        I18N i18n;
        public I18N I18N
        {
            get => i18n;
            set => this.RaiseAndSetIfChanged(ref i18n, value);
        }

        void HandleI18NChanged(I18N i18n)
        {
            I18N = i18n;
        }

        public BaseContext()
        {
            BaseContextManager.Instance.I18NChanged += HandleI18NChanged;
        }

        public virtual void Dispose()
        {
            BaseContextManager.Instance.I18NChanged -= HandleI18NChanged;
        }
    }

    public class BaseContextManager
    {
        public event Action<I18N> I18NChanged;
        public I18N I18N
        {
            set
            {
                I18NChanged?.Invoke(value);
            }
        }

        static BaseContextManager instance;

        public static BaseContextManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BaseContextManager();
                }
                return instance;
            }
        }
    }
    
}
