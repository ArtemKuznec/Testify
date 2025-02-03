using System;
using System.Drawing;

namespace Testify.Core.Interfaces
{
    public interface IProperties
    {
        string? GetAttribute(string name, TimeSpan? timeout = null);

        string? GetProperty(string name, TimeSpan? timeout = null);

        string? GetValue(TimeSpan? timeout = null);

        string? GetClass(TimeSpan? timeout = null);

        string GetText(TimeSpan? timeout = null);

        string GetTag(TimeSpan? timeout = null);

        Point GetLocation(TimeSpan? timeout = null);

        Size GetSize(TimeSpan? timeout = null);

        bool IsDisplayed(TimeSpan? timeout = null);

        bool IsSelected(TimeSpan? timeout = null);

        bool IsReadOnly(TimeSpan? timeout = null);

        bool IsEnabled(TimeSpan? timeout = null);
    }
}
