using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;

/// <summary>
/// Show every image with the exif info.
/// </summary>
public partial class PicShower : System.Web.UI.UserControl
{
    // Physical path for the image to be displayed
    private string physicalImagePath = "";    

    /// <summary>
    /// Get or Set the image's physical path which will be displayed
    /// </summary>
    public string PhysicalImagePath
    {
        get
        {
            return physicalImagePath;
        }
        set
        {
            physicalImagePath = value;
            image.ImageUrl = "Image.aspx?path=" + System.Web.HttpUtility.UrlEncode(physicalImagePath);
            image.NavigateUrl = "Image.aspx?mode=FULL&path=" + System.Web.HttpUtility.UrlEncode(physicalImagePath);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
}
