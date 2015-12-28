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
using System.Drawing.Imaging;

/// <summary>
/// This page is used to load image file from physical disk to memory stream and output as a http stream.
/// </summary>
public partial class Image : System.Web.UI.Page
{
    public enum DisplayModes
    {
        Thumbnail,
        Full
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get physical path from url
        string physicalImagePath = Request.QueryString["path"];
        // Get display moden
        DisplayModes mode = DisplayModes.Thumbnail;
        if (Request.QueryString["mode"] != null && Request.QueryString["mode"].Trim().ToUpper() == "FULL")
        {
            mode = DisplayModes.Full;
        }

        // Check path
        if (physicalImagePath == null || physicalImagePath.Length == 0 || !File.Exists(physicalImagePath))
        {
            // If can not get the path or get a empty path or get a wrong path, then do noting
            return;
        }

        if (mode != DisplayModes.Full && Holder.ContainsPhysicalImagePath(physicalImagePath))
        {
            try
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Set thumbnail quality
                    EncoderParameters encoderPara = new EncoderParameters(1);
                    encoderPara.Param[0] = new EncoderParameter(Encoder.Quality, Convert.ToInt64(Consts.ThumbnailImageQuality));
                    ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(Consts.ThumbnailImageMimeType);

                    // Put image to the temp space with jpg format
                    Holder.GetImageByPhysicalImagePath(physicalImagePath).Save(imageStream, imageCodecInfo, encoderPara);
                    // Clear the reponse buffer
                    Response.Clear();
                    // Set the http stream as a jpg file
                    Response.ContentType = Consts.ThumbnailImageMimeType;
                    // Put the image stream to the http stream from temp space from Holder
                    imageStream.WriteTo(Response.OutputStream);
                }
                return;
            }
            catch (Exception ex_)
            {
                // do nothing
                ;
            }
        }

        // Temp space for the image file
        using (MemoryStream imageStream = new MemoryStream())
        {            
            try
            {
                // Load image file from physical disk
                System.Drawing.Image image = System.Drawing.Image.FromFile(physicalImagePath);

                if (mode == DisplayModes.Full)
                {

                    using (FileStream fs = new FileStream(physicalImagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, Convert.ToInt32(fs.Length));

                        // Clear the reponse buffer
                        Response.Clear();
                        // Set the http stream as a jpg file
                        Response.ContentType = Consts.ThumbnailImageMimeType;
                        // Put the image stream to the http stream from temp space
                        Response.BinaryWrite(buffer);
                    }
                }
                else
                {
                    using (Stream bmpImage = new MemoryStream())
                    {
                        if (!Consts.UseExifThumbnailImage)
                        {
                            // Reload image as BitMap format
                            // The JPG file which with EXIF format maybe include a ThumbnailImage
                            image.Save(bmpImage, ImageFormat.Bmp);
                            image = System.Drawing.Image.FromStream(bmpImage);
                        }

                        //init thumbnail object
                        System.Drawing.Image imageThumbnail = null;

                        // Get the thumbnail image
                        imageThumbnail = image.GetThumbnailImage(Consts.ThumbnailImageWidth,
                            Convert.ToInt32((Convert.ToDouble(Consts.ThumbnailImageWidth) / image.Width) * image.Height),
                            new System.Drawing.Image.GetThumbnailImageAbort(ReturnFalse),
                            IntPtr.Zero);
                        // Save to Holder
                        Holder.AddImage(physicalImagePath, imageThumbnail);

                        // Set thumbnail quality
                        EncoderParameters encoderPara = new EncoderParameters(1);
                        encoderPara.Param[0] = new EncoderParameter(Encoder.Quality, Convert.ToInt64(Consts.ThumbnailImageQuality));
                        ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(Consts.ThumbnailImageMimeType);

                        // Put image to the temp space with jpg format
                        imageThumbnail.Save(imageStream, imageCodecInfo, encoderPara);
                        
                        // Clear the reponse buffer
                        Response.Clear();
                        // Set the http stream as a jpg file
                        Response.ContentType = Consts.ThumbnailImageMimeType;
                        // Put the image stream to the http stream from temp space
                        imageStream.WriteTo(Response.OutputStream);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
                // If any exception be throwed, then do nothing
                return;
            }
        }
    }

    private bool ReturnFalse()
    {
        return false;
    }

    private ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;

        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }
}
