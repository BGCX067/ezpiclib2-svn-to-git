using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Consts の概要の説明です
/// </summary>
public class Consts
{
    private static bool isReadedFromConfig = false;

    public const string ThumbnailImageMimeType = "image/jpeg";

    private static int thumbnailImageWidth = 0;
    public static int ThumbnailImageWidth
    {
        get
        {
            if (!isReadedFromConfig)
            {
                ReadFromConfig();
            }
            return thumbnailImageWidth;
        }
    }

    private static int imageTableColNum = 0;
    public static int ImageTableColNum
    {
        get
        {
            if (!isReadedFromConfig)
            {
                ReadFromConfig();
            }
            return imageTableColNum;
        }
    }

    private static string libRootPath = "";
    public static string LibRootPath
    {
        get
        {
            if (!isReadedFromConfig)
            {
                ReadFromConfig();
            }
            return libRootPath;
        }
    }

    private static int thumbnailImageQuality = 0;
    public static int ThumbnailImageQuality
    {
        get
        {
            if (!isReadedFromConfig)
            {
                ReadFromConfig();
            }
            return thumbnailImageQuality;
        }
    }

    private static bool useExifThumbnailImage = false;
    public static bool UseExifThumbnailImage
    {
        get
        {
            if (!isReadedFromConfig)
            {
                ReadFromConfig();
            }
            return useExifThumbnailImage;
        }
    }

    private static void ReadFromConfig()
    {
        try
        {
            thumbnailImageWidth = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["ThumbnailImageWidth"]);
        }
        catch
        { }
        finally
        {
            if (thumbnailImageWidth <= 0)
            {
                thumbnailImageWidth = 200;
            }
        }

        try
        {
            imageTableColNum = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["ImageTableColNum"]);
        }
        catch
        { }
        finally
        {
            if (imageTableColNum <= 0)
            {
                imageTableColNum = 4;
            }
        }

        try
        {
            libRootPath = System.Web.Configuration.WebConfigurationManager.AppSettings["LibRootPath"];
        }
        catch
        { }

        try
        {
            thumbnailImageQuality = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["ThumbnailImageQuality"]);
        }
        catch
        { }
        finally
        {
            if (thumbnailImageQuality <= 0)
            {
                thumbnailImageQuality = 80;
            }
        }

        try
        {
            if ((System.Web.Configuration.WebConfigurationManager.AppSettings["UseExifThumbnailImage"]).ToUpper() == "FALSE")
            {
                useExifThumbnailImage = false;
            }
            else
            {
                useExifThumbnailImage = true;
            }
        }
        catch
        {
            useExifThumbnailImage = true;
        }

        isReadedFromConfig = true;
    }

	public Consts()
	{

	}
}
