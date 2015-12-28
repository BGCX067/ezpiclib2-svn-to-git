using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;

/// <summary>
/// Holder の概要の説明です
/// </summary>
public class Holder
{
    private static Hashtable holder = new Hashtable();
	
    public Holder()
	{
		//
		// TODO: コンストラクタ ロジックをここに追加します
		//
	}

    public void Clear()
    {
        holder.Clear();
    }

    public static System.Drawing.Image GetImageByPhysicalImagePath(string physicalImagePath_)
    {
        if(holder.ContainsKey(physicalImagePath_))
        {
            return ((System.Drawing.Image)holder[physicalImagePath_]);
        }
        else
        {
            return null;
        }
    }

    public static bool ContainsPhysicalImagePath(string physicalImagePath_)
    {
        return holder.ContainsKey(physicalImagePath_);
    }

    // Insert or update image by physicalImagePath
    public static void AddImage(string physicalImagePath_, System.Drawing.Image image_)
    {
        if(holder.ContainsKey(physicalImagePath_))
        {
            // Update
            holder[physicalImagePath_] = image_;
        }
        else
        {
            // New insert
            holder.Add(physicalImagePath_, image_);
        }
    }
}
