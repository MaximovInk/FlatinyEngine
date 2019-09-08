using System;

namespace MaximovInk.FlatinyEngine.Core.Compnents
{
    public interface IComponent
    {
        bool enabled { get; set; }
        GameObject gameObject { get; set; }
        string tag { get; set; }
    }
}