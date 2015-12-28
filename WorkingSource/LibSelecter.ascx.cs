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

public partial class LibSelecter : System.Web.UI.UserControl
{
    /// <summary>
    /// The root full path for lib's store
    /// </summary>
    private static string libRootPath = null;
    /// <summary>
    /// The name of the curr lib
    /// </summary>
    private string currLibName = null;
    /// <summary>
    /// The error message.
    /// </summary>
    private string errorMessage = "";

    /// <summary>
    /// Read or Set the root full path for lib's store. <br />
    /// Every time reset this propertie will reset the whole LibSelecter.
    /// </summary>
    public string LibRootPath
    {
        get
        {
            return libRootPath;
        }
        set
        {
            // Reset the whole LibSelecter
            libRootPath = null;
            LibSelect.Enabled = false;
            errorMessage = "";
            LibSelect.Items.Clear();

            if (Directory.Exists(value))
            {
                // Setted path exists
                libRootPath = value;
                LibSelect.Enabled = true;                
                
                DirectoryInfo libRoot = new DirectoryInfo(libRootPath);
                // Put every lib to the lib select
                foreach (DirectoryInfo lib in libRoot.GetDirectories())
                {
                    LibSelect.Items.Add(lib.Name);
                }
            }
            else
            {
                // Setted path not exists                
                ShowError("配置的Lib Root路径不存在。");
            }

        }
    }

    /// <summary>
    /// Read or Set the default(curr) lib's name.
    /// </summary>
    public string CurrLibName
    {
        get
        {
            currLibName = LibSelect.Items[LibSelect.SelectedIndex].Text;
            return currLibName;
        }
        set
        {
            currLibName = null;
            if (LibSelect.Enabled == false)
            {
                throw new Exception("不能在LibSelect不可用的状态下设定本属性。");
            }
            else
            {
                if (LibSelect.Items.FindByText(value) == null)
                {
                    throw new Exception("设定的CurrLibName在当前LibSelect中不存在。");
                }
                else
                {
                    currLibName = value;
                    LibSelect.Items.FindByText(currLibName).Selected = true;
                }
            }
        }
    }

    public string PhysicalLibPath
    {
        get
        {
            return (LibRootPath + "\\" + CurrLibName);
        }
    }

    /// <summary>
    /// Add new error message to the label
    /// </summary>
    /// <param name="errorMessage_"></param>
    private void ShowError(string errorMessage_)
    {
        if (errorMessage.Length != 0)
        {
            errorMessage_ = "\r\n" + errorMessage_;
        }
        errorMessage += errorMessage_;
        LibSelecterError.Text = errorMessage_;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void LibSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CurrLibName = LibSelect.SelectedItem.Text;
    }
}
