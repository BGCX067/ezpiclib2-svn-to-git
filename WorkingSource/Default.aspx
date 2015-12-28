<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="PicLister.ascx" TagName="PicLister" TagPrefix="uc3" %>

<%@ Register Src="PicShower.ascx" TagName="PicShower" TagPrefix="uc2" %>

<%@ Register Src="LibSelecter.ascx" TagName="LibSelecter" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>.:EZPicLib2:. Powered by Microsoft ASP.NET 2.0</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        请仅下载自己照片，不要下载其他同志的照片或对照片进行任何处理，谢谢。<br />
        Powered By <a href="mailto:jamesw@wicresoft.com">James Weng</a> &amp; <a href="mailto:miaoh@wicresoft.com">Miao Hua</a><br />
        <uc1:LibSelecter ID="LibSelecter1" runat="server" />
        <br />
        <uc3:PicLister id="PicLister1" runat="server">
        </uc3:PicLister></div>
        <img src="pow_by_aspnet2_0a.gif" />
    </form>
</body>
</html>
