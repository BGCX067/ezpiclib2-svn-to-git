using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Page.IsPostBack)
        {
        }
        else
        {
            //Consts.PhysicalApplicationPath = Request.PhysicalApplicationPath;
            this.LibSelecter1.LibRootPath = Consts.LibRootPath;
        }
        
        this.PicLister1.PhysicalLibRootPath = this.LibSelecter1.PhysicalLibPath;
    }
}
