using System;
using BlogEngine.Core.Web.Controls;

namespace BlogEngine.NET
{
    public partial class CreateGUIDsOnline : BlogBasePage
    {
        protected string guidSring = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            guidSring = Guid.NewGuid().ToString();
        }
    }
}