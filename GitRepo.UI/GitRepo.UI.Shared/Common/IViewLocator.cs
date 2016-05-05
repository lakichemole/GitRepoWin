using System;
using System.Collections.Generic;
using System.Text;

namespace GitRepo.UI.Common
{
    public interface IViewLocator
    {
        Type GetViewFromViewKey(int viewKey);
    }
}
