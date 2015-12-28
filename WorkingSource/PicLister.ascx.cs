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
using System.IO;

/// <summary>
/// Display the whole Lib.
/// </summary>
public partial class PicLister : System.Web.UI.UserControl
{
    // The Lib's physical path
    private string physicalLibRootPath = "";
    // The Lib folder's object
    private DirectoryInfo libRootFolder = null;

    /// <summary>
    /// Get or Set the Lib's physical path which will be displayed
    /// </summary>
    public string PhysicalLibRootPath
    {
        get
        {
            return physicalLibRootPath;
        }
        set
        {
            physicalLibRootPath = value;
            libRootFolder = new DirectoryInfo(physicalLibRootPath);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int imageTableRowNum = 0;
        FileInfo[] imageFiles = libRootFolder.GetFiles("*.jpg");
        imageTableRowNum = (imageFiles.Length / Consts.ImageTableColNum) + 1;

        while (DispTable.Rows.Count < imageTableRowNum)
        {
            TableRow tr = new TableRow();
            while(tr.Cells.Count < Consts.ImageTableColNum)
            {
                TableCell tc = new TableCell();
                tr.Cells.Add(tc);
            }
            DispTable.Rows.Add(tr);
        }

        int rowPoint = 0;
        int cellPoint = 0;

        foreach (FileInfo imageFile in libRootFolder.GetFiles("*.jpg"))
        {            
            PicShower picShower = (PicShower)LoadControl("PicShower.ascx");
            picShower.PhysicalImagePath = imageFile.FullName;
            DispTable.Rows[rowPoint].Cells[cellPoint++].Controls.Add(picShower);
            
            if (cellPoint == Consts.ImageTableColNum)
            {
                cellPoint = 0;
                rowPoint++;
            }
        }

    }
}
