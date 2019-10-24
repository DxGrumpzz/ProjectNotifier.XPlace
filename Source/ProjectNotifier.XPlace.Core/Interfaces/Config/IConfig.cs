namespace ProjectNotifier.XPlace.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public interface IConfig
    {

        public T GetValue<T>(string settingName);

        public void SetValue<T>(string settingName, T value);

    };
}
